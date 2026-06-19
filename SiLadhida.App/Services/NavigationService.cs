using System;
using Avalonia.Controls;

namespace SiLadhida.App.Services;

public class NavigationService
{
    public event Action<UserControl>? OnViewChanged;
    public event Action<string>? OnTitleChanged;

    public void Navigate(
        UserControl view,
        string title)
    {
        OnTitleChanged?.Invoke(title);
        OnViewChanged?.Invoke(view);
    }

    public void SetTitle(string title)
    {
        OnTitleChanged?.Invoke(title);
    }
}