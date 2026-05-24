using System;
using Avalonia.Controls;

namespace SiLadhida.App.Services;

public class NavigationService
{
    public event Action<UserControl>? OnViewChanged;

    public void Navigate(UserControl view)
    {
        OnViewChanged?.Invoke(view);
    }
}