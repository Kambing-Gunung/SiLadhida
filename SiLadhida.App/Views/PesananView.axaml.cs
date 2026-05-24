using System;
using Avalonia.Controls;
using SiLadhida.App.Services;

namespace SiLadhida.App.Views;

public partial class PesananView : UserControl
{
    private readonly OrderApiService _service;

    public PesananView()
    {
        InitializeComponent();

        _service = new OrderApiService();

        LoadOrders();
    }

    private async void LoadOrders()
    {
        try
        {
            var data = await _service.GetOrdersAsync();

            if (data != null)
            {
                OrderListBox.ItemsSource = data;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"ERROR LOAD ORDERS: {ex.Message}"
            );
        }
    }
}