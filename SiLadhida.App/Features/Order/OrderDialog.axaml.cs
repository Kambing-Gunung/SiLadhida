using Avalonia.Controls;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Features.Order;

public partial class OrderDialog : UserControl
{
    private BaseDialog? _dialog;

    public OrderDialog(Order order)
    {
        InitializeComponent();

        var vm = new OrderDialogViewModel(order);
        DataContext = vm;

        vm.RequestClose += async (result) =>
        {
            if (_dialog != null)
            {
                await _dialog.CloseWithAnimation(result);
            }
        };
    }

    // 🔥 dipanggil dari luar
    public void AttachDialog(BaseDialog dialog)
    {
        _dialog = dialog;
    }
}