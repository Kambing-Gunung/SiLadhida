using System;

namespace SiLadhida.App.Services.App;

public class NotificationService
{
    public event Action<string>? OnSuccess;

    public event Action<string>? OnError;

    public void ShowSuccess(string message)
    {
        OnSuccess?.Invoke(message);
    }

    public void ShowError(string message)
    {
        OnError?.Invoke(message);
    }
}