using System;

namespace SiLadhida.App.Services;

public class NavigationService
{
    public event Action<object>? OnViewChanged;

    public void Navigate(object view)
    {
        OnViewChanged?.Invoke(view);
    }
}