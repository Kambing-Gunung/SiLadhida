using Avalonia;
using Avalonia.Controls;

namespace SiLadhida.App.Components.Feedback;

public partial class EmptyState : UserControl
{
    public static readonly StyledProperty<string> IconProperty =
        AvaloniaProperty.Register<EmptyState, string>(
            nameof(Icon), "📦");

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<EmptyState, string>(
            nameof(Title), "");

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<EmptyState, string>(
            nameof(Description), "");

    public string Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public EmptyState()
    {
        InitializeComponent();
    }
}