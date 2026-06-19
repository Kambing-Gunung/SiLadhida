using Avalonia.Controls;
using SiLadhida.App.Models;
using System.Collections.ObjectModel;
using SiLadhida.App.Services;
using System;
using System.Threading.Tasks;
using SiLadhida.App.Models.Requests;
using System.Collections.Generic;
using System.Linq;

namespace SiLadhida.App.Components.Dialogs;

public partial class OrderDialog : UserControl
{
    private readonly Order _order;
    private List<Product> _products = new();
    private readonly ProductApiService _productService = new();
    private readonly OrderApiService _orderService = new();

    public OrderFormModel ViewModel { get; }

    public OrderDialog(Order order)
    {
        InitializeComponent();

        _order = order;

        ViewModel = new OrderFormModel
        {
            NamaPemesan = order.NamaPemesan,
            StatusSekarang = order.StatusSekarang.ToString()
        };

        DataContext = ViewModel;

        Loaded += async (_, __) =>
        {
            await InitAsync(order);
        };
    }

    private async Task InitAsync(Order order)
    {
        try
        {
            _products = await _productService.GetProductsAsync();

            LoadItems(order);
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    private void LoadItems(Order order)
    {
        ViewModel.Items.Clear();

        foreach (var item in order.Items)
        {
            ViewModel.Items.Add(CreateItemVM(item));
        }

        ViewModel.RefreshTotal();
    }

    private OrderItemFormModel CreateItemVM(OrderItem item)
    {
        var product = _products
            .FirstOrDefault(p => p.Id == item.ProductId);

        var nama = product?.Nama
            ?? item.NamaProduk
            ?? $"Produk #{item.ProductId}";

        return new OrderItemFormModel(
            ViewModel.RefreshTotal,

            // 🔥 INCREASE
            async () =>
            {
                await _orderService.IncreaseItemAsync(
                    _order.Id,
                    item.ProductId,
                    new UpdateQuantityRequest { Quantity = 1 });

                await RefreshOrder();
            },

            // 🔥 DECREASE
            async () =>
            {
                await _orderService.DecreaseItemAsync(
                    _order.Id,
                    item.ProductId,
                    new UpdateQuantityRequest { Quantity = 1 });

                await RefreshOrder();
            },

            // 🔥 REMOVE
            async () =>
            {
                await _orderService.RemoveItemAsync(
                    _order.Id,
                    item.ProductId);

                await RefreshOrder();
            }
        )
        {
            ProductId = item.ProductId,
            NamaProduk = nama,
            Harga = item.Harga,
            Quantity = item.Quantity
        };
    }

    // 🔥 ADD ITEM
    private async void AddItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var products = await _productService.GetProductsAsync();

        var dialog = new OrderItemDialog(
            new ObservableCollection<Product>(products));

        var host = new Window
        {
            Content = dialog,
            Width = 600,     // 🔥 tambah lebar
            Height = 650,    // 🔥 tambah tinggi
            MinWidth = 500,
            MinHeight = 500,
            Title = "Pilih Produk",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            WindowDecorations = WindowDecorations.None
        };

        var mainWindow = TopLevel.GetTopLevel(this) as Window;

        if (mainWindow == null)
            return;

        var selectedProducts = await host.ShowDialog<List<Product>?>(mainWindow);

        if (selectedProducts == null || !selectedProducts.Any())
            return;

        try
        {
            foreach (var product in selectedProducts)
            {
                await _orderService.AddItemAsync(
                    _order.Id,
                    new AddOrderItemRequest
                    {
                        ProductId = product.Id,
                        Quantity = 1
                    });
            }

            await RefreshOrder(); // 🔥 WAJIB
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    // 🔥 INCREASE
    private async void Increase_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.Tag is not OrderItemFormModel item)
            return;

        try
        {
            await _orderService.IncreaseItemAsync(
                _order.Id,
                item.ProductId,
                new UpdateQuantityRequest { Quantity = 1 });

            await RefreshOrder();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    // 🔥 DECREASE
    private async void Decrease_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.Tag is not OrderItemFormModel item)
            return;

        try
        {
            await _orderService.DecreaseItemAsync(
                _order.Id,
                item.ProductId,
                new UpdateQuantityRequest { Quantity = 1 });

            await RefreshOrder();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    private async Task RefreshOrder()
    {
        var updated = await _orderService.GetOrderAsync(_order.Id);

        if (updated == null)
            return;

        _order.Items = updated.Items;

        if (!_products.Any())
        {
            _products = await _productService.GetProductsAsync();
        }

        if (DataContext is OrderFormModel vm)
        {
            foreach (var updatedItem in updated.Items)
            {
                var existing = vm.Items
                    .FirstOrDefault(x => x.ProductId == updatedItem.ProductId);

                if (existing != null)
                {
                    // 🔥 UPDATE existing (INI KUNCI)
                    existing.Quantity = updatedItem.Quantity;
                    existing.Harga = updatedItem.Harga;
                }
                else
                {
                    // 🔥 ADD baru
                    vm.Items.Add(CreateItemVM(updatedItem));
                }
            }

            // 🔥 REMOVE item yang sudah tidak ada
            var toRemove = vm.Items
                .Where(x => !updated.Items.Any(u => u.ProductId == x.ProductId))
                .ToList();

            foreach (var item in toRemove)
            {
                vm.Items.Remove(item);
            }

            vm.RefreshTotal();
        }
    }

    private async Task LoadProductsAndInit(Order order)
    {
        try
        {
            _products = await _productService.GetProductsAsync();

            LoadItems(order); // 🔥 baru init item setelah produk ada
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    private void Close_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close();
    }
}