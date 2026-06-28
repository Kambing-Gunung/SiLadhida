using Avalonia;
using Avalonia.Controls;

namespace SiLadhida.App.Components.Inputs;

public partial class SearchBox : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<SearchBox, string>(
            nameof(Text), "");

    public static readonly StyledProperty<string> PlaceholderProperty =
        AvaloniaProperty.Register<SearchBox, string>(
            nameof(Placeholder), "Cari...");

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public SearchBox()
    {
        InitializeComponent();
    }
}