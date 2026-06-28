using SiLadhida.App.Services.Api;
using SiLadhida.App.Services.App;
using SiLadhida.App.Core.Api;
using SiLadhida.App.Core.Auth;
using SiLadhida.App.Services.Observer;

namespace SiLadhida.App.Services;

public class ServiceLocator
{
    public AuthSession AuthSession { get; private set; }
    public ApiClient ApiClient { get; private set; } 

    public AuthService AuthService { get; private set; } 
    public ProductService ProductService { get; private set; } 
    public OrderService OrderService { get; private set; } 

    public NavigationService Navigation { get; private set; } 
    public NotificationService Notification { get; private set; } 
    public LoadingService Loading { get; private set; } 
    public DialogService Dialog { get; private set; } 

    public NotificationPublisher NotificationPublisher { get; private set; } 

    public ServiceLocator()
    {
        AuthSession = AuthSession.Instance;
        ApiClient = ApiClient.Instance;

        AuthService = AuthService.Instance;
        ProductService = ProductService.Instance;
        OrderService = OrderService.Instance;

        Navigation = NavigationService.Instance;
        Notification = NotificationService.Instance;
        Loading = LoadingService.Instance;
        Dialog = DialogService.Instance;

        NotificationPublisher = new NotificationPublisher(); 
        var logger = new SystemLogger(); 
        NotificationPublisher.Subscribe(logger);
        NotificationPublisher.Subscribe(Notification); 
    }
}