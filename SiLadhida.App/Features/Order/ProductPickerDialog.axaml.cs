using Avalonia.Controls;
using SiLadhida.App.Components.Dialogs;

using ProductEntity = SiLadhida.App.Features.Product.Product;

namespace SiLadhida.App.Features.Order;

public partial class ProductPickerDialog : UserControl
{
    private BaseDialog? _dialog;

    public ProductPickerDialog()
    {
        InitializeComponent();

        var vm = new ProductPickerViewModel();
        DataContext = vm;

        vm.RequestClose += async (result) =>
        {
            if (_dialog != null)
            {
                await _dialog.CloseWithAnimation(result);
            }
        };
    }

    public void AttachDialog(BaseDialog dialog)
    {
        _dialog = dialog;
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is ProductPickerViewModel vm && sender is DataGrid grid)
        {
            vm.SelectedProducts.Clear();

            foreach (var item in grid.SelectedItems)
            {
                if (item is ProductEntity p)
                    vm.SelectedProducts.Add(p);
            }
        }
    }
}