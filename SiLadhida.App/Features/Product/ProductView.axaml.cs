using Avalonia.Controls;
using Avalonia.VisualTree;

namespace SiLadhida.App.Features.Product;

public partial class ProductView : UserControl
{
    public ProductView()
    {
        InitializeComponent();

        this.AttachedToVisualTree += async (_, __) =>
        {
            if (DataContext is ProductViewModel vm)
            {
                await vm.InitializeAsync();
            }
        };
    }
}