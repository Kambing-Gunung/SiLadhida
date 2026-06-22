using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace SiLadhida.App.Models;

public partial class OrderItemFormModel : ObservableObject
{
    private readonly Func<Task> _increase;
    private readonly Func<Task> _decrease;
    private readonly Func<Task> _remove;
    private readonly Action _refresh;

    public OrderItemFormModel(
        Action refresh,
        Func<Task> increase,
        Func<Task> decrease,
        Func<Task> remove)
    {
        _refresh = refresh;
        _increase = increase;
        _decrease = decrease;
        _remove = remove;
    }

    public int ProductId { get; set; }

    [ObservableProperty]
    private string namaProduk = string.Empty;

    [ObservableProperty]
    private decimal harga;

    [ObservableProperty]
    private int quantity = 1;

    public decimal SubTotal => Harga * Quantity;

    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(SubTotal));
        _refresh();
    }

    [RelayCommand]
    private async Task Increase()
    {
        await _increase();
    }

    [RelayCommand]
    private async Task Decrease()
    {
        await _decrease();
    }

    [RelayCommand]
    private async Task Remove()
    {
        await _remove();
    }
}