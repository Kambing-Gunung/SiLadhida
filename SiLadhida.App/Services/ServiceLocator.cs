using SiLadhida.App.Services.Api;
using SiLadhida.App.Services.App;
using SiLadhida.App.Core.Api;
using SiLadhida.App.Core.Auth;

namespace SiLadhida.App.Services;

public class ServiceLocator
{
    public AuthSession AuthSession { get; private set; } = null!;
    public ApiClient ApiClient { get; private set; } = null!;

    public AuthService AuthService { get; private set; } = null!;
    public ProductService ProductService { get; private set; } = null!;
    public OrderService OrderService { get; private set; } = null!;

    public DialogService Dialog { get; private set; } = null!;
    public LoadingService Loading { get; private set; } = null!;
    public NavigationService Navigation { get; private set; } = null!;
    public NotificationService Notification { get; private set; } = null!;

    public void Initialize()
    {
        // Core
        AuthSession = new AuthSession();

        ApiClient = ApiClient.Instance;
        ApiClient.SetSession(AuthSession);

        // API
        AuthService = new AuthService(ApiClient, AuthSession);
        ProductService = new();
        OrderService = new();

        // App
        Dialog = new DialogService();
        Loading = new LoadingService();
        Navigation = new NavigationService();
        Notification = new NotificationService();
    }
}