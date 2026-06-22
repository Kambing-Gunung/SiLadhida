using SiLadhida.App.Services.Api;
using SiLadhida.App.Services.App;
using SiLadhida.App.Core.Api;
using SiLadhida.App.Core.Auth;

namespace SiLadhida.App.Services;

public class ServiceLocator
{
    public AuthSession AuthSession { get; }
    public ApiClient ApiClient { get; }

    public AuthService AuthService { get; }
    public ProductService ProductService { get; }
    public OrderService OrderService { get; }

    public DialogService Dialog { get; }
    public LoadingService Loading { get; }
    public NavigationService Navigation { get; }
    public NotificationService Notification { get; }

    public ServiceLocator()
    {
        // Core
        AuthSession = new AuthSession();
        ApiClient = new ApiClient(AuthSession);

        // API
        AuthService = new AuthService(ApiClient, AuthSession);
        ProductService = new ProductService(ApiClient);
        OrderService = new OrderService(ApiClient);

        // App
        Dialog = new DialogService();
        Loading = new LoadingService();
        Navigation = new NavigationService();
        Notification = new NotificationService();
    }
}