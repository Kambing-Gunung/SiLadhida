using Avalonia.Controls;
using Avalonia.Animation;
using Avalonia.VisualTree;
using Avalonia.Animation.Easings;
using System;
using System.Threading.Tasks;
using Avalonia.Styling;
using Avalonia.Media;
using Avalonia.Input;

namespace SiLadhida.App.Components.Dialogs;

public partial class BaseDialog : Window
{
    public BaseDialog()
    {
        InitializeComponent();

        Opened += async (_, __) => await PlayOpenAnimation();
    }

    public void SetContent(Control content)
    {
        Console.WriteLine("Control: " + content);
        
        if (content.Parent != null)
            throw new InvalidOperationException("Content already has a parent.");

        DialogContent.Content = content;
    }

    private async Task PlayOpenAnimation()
    {
        var scale = DialogCard.RenderTransform as ScaleTransform;

        // initial
        DialogCard.Opacity = 0;
        Overlay.Opacity = 0;

        if (scale != null)
        {
            scale.ScaleX = 0.9;
            scale.ScaleY = 0.9;
        }

        await Task.Delay(10); // allow layout render

        // animate (manual step)
        for (int i = 0; i <= 10; i++)
        {
            double t = i / 10.0;

            DialogCard.Opacity = t;
            Overlay.Opacity = t * 0.6;

            if (scale != null)
            {
                scale.ScaleX = 0.9 + (0.1 * t);
                scale.ScaleY = 0.9 + (0.1 * t);
            }

            await Task.Delay(10);
        }
    }

    public async Task CloseWithAnimation(object? result = null)
    {
        var scale = DialogCard.RenderTransform as ScaleTransform;

        for (int i = 10; i >= 0; i--)
        {
            double t = i / 10.0;

            DialogCard.Opacity = t;
            Overlay.Opacity = t * 0.6;

            if (scale != null)
            {
                scale.ScaleX = 0.9 + (0.1 * t);
                scale.ScaleY = 0.9 + (0.1 * t);
            }

            await Task.Delay(10);
        }

        Close(result);
    }

    private async void Overlay_Click(object? sender, PointerPressedEventArgs e)
    {
        await CloseWithAnimation(false);
    }

    protected override async void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            await CloseWithAnimation(false);
        }

        base.OnKeyDown(e);
    }
}