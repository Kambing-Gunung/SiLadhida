using Avalonia.Controls;
using Avalonia.VisualTree;

namespace SiLadhida.App.Features.Transaction;

public partial class TransactionView : UserControl
{
    public TransactionView()
    {
        InitializeComponent();

        this.AttachedToVisualTree += async (_, __) =>
        {
            if (DataContext is TransactionViewModel vm)
            {
                await vm.LoadTransactionsAsync();
            }
        };
    }
}