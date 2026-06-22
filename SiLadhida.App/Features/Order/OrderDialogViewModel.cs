using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SiLadhida.App.Shared.Requests;

using ProductEntity = SiLadhida.App.Features.Product.Product;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Features.Order;

public partial class OrderDialogViewModel : ObservableObject
{
    private Order _order;

    [ObservableProperty]
    private string namaPemesan = "";

    [ObservableProperty]
    private string status = "";

    [ObservableProperty]
    private ObservableCollection<OrderItem> items = new();

    [ObservableProperty]
    private decimal totalHarga;

    // 🔥 EVENT CLOSE
    public event Action<object?>? RequestClose;

    public OrderDialogViewModel(Order order)
    {
        _order = order;

        NamaPemesan = order.NamaPemesan;
        Status = order.Status;
        Items = new ObservableCollection<OrderItem>(order.Items);
        TotalHarga = order.TotalHarga;

        HookItems();

        _ = InitializeAsync();
    }

    // 🔥 CLOSE
    [RelayCommand]
    private async Task Close()
    {
        RequestClose?.Invoke(null);
        await Task.CompletedTask;
    }

    // 🔥 ADD ITEM
    [RelayCommand]
    private async Task AddItemAsync()
    {
        try
        {
            var content = new ProductPickerDialog();
            var dialog = new BaseDialog();

            content.AttachDialog(dialog);
            dialog.SetContent(content);

            var result = await App.Services.Dialog
                .ShowAsync<ObservableCollection<ProductEntity>>(dialog);

            if (result == null || result.Count == 0)
                return;

            foreach (var p in result)
            {
                var existing = Items.FirstOrDefault(x => x.ProductId == p.Id);

                if (existing != null)
                {
                    await App.Services.OrderService.UpdateItemAsync(
                        _order.Id,
                        p.Id,
                        new UpdateQuantityRequest
                        {
                            Quantity = existing.Quantity + 1
                        });
                }
                else
                {
                    await App.Services.OrderService.AddItemAsync(
                        _order.Id,
                        new AddOrderItemRequest
                        {
                            ProductId = p.Id,
                            Quantity = 1
                        });
                }
            }

            await ReloadOrder();
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task IncreaseQty(OrderItem item)
    {
        try
        {
            await App.Services.OrderService.UpdateItemAsync(
                _order.Id,
                item.ProductId,
                new UpdateQuantityRequest
                {
                    Quantity = item.Quantity + 1
                });

            await ReloadOrder();
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task DecreaseQty(OrderItem item)
    {
        try
        {
            var newQty = item.Quantity - 1;

            if (newQty < 0)
                return;

            await App.Services.OrderService.UpdateItemAsync(
                _order.Id,
                item.ProductId,
                new UpdateQuantityRequest
                {
                    Quantity = newQty
                });

            await ReloadOrder();
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task RemoveItem(OrderItem item)
    {
        try
        {
            await App.Services.OrderService.UpdateItemAsync(
                _order.Id,
                item.ProductId,
                new UpdateQuantityRequest
                {
                    Quantity = 0 // 🔥 API auto delete
                });

            await ReloadOrder();
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
    }

    // 🔥 RELOAD
    private async Task ReloadOrder()
    {
        Console.WriteLine("ReloadOrder jalan");
        var updated = await App.Services.OrderService.GetOrderAsync(_order.Id);

        if (updated == null)
            return;

        _order = updated;

        UnhookItems();

        Items = new ObservableCollection<OrderItem>(_order.Items);

        // 🔥 AMBIL SEMUA PRODUK SEKALI
        var products = await App.Services.ProductService.GetProductsAsync();

        foreach (var item in Items)
        {
            var match = products.FirstOrDefault(p => p.Id == item.ProductId);
            item.NamaProduk = match?.Nama ?? "Unknown";
            Console.WriteLine($"Mapping: {item.ProductId}");
            Console.WriteLine($"NamaProduk: {item.NamaProduk}");
        }

        HookItems();

        TotalHarga = _order.TotalHarga;
        Status = _order.Status;
    }

    // 🔥 EVENT HANDLING
    private void HookItems()
    {
        foreach (var item in Items)
        {
            item.PropertyChanged += ItemChanged;
        }
    }

    private void UnhookItems()
    {
        foreach (var item in Items)
        {
            item.PropertyChanged -= ItemChanged;
        }
    }

    private void ItemChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        TotalHarga = Items.Sum(x => x.SubTotal);
    }

    private async Task InitializeAsync()
    {
        var products = await App.Services.ProductService.GetProductsAsync();

        foreach (var item in Items)
        {
            var match = products.FirstOrDefault(p => p.Id == item.ProductId);
            item.NamaProduk = match?.Nama ?? "Unknown";
        }
    }
}