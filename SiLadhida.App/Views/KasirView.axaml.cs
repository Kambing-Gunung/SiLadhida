using Avalonia.Controls;
using SiLadhida.App.ViewModels;

namespace SiLadhida.App.Views;

public partial class KasirView : UserControl
{
    public KasirView()
    {
        InitializeComponent();

        DataContext = new KasirViewModel();
    }
}