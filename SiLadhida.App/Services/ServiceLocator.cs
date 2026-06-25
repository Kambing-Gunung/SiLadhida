using SiLadhida.App.Services.Api;
using SiLadhida.App.Services.App;
using SiLadhida.App.Core.Api;
using SiLadhida.App.Core.Auth;
using SiLadhida.App.Services.Observer;

namespace SiLadhida.App.Services;

public class ServiceLocator
{
    // Core
    public AuthSession AuthSession { get; private set; } = null!;
    public ApiClient ApiClient { get; private set; } = null!;

    // API
    public AuthService AuthService { get; private set; } = null!;
    public ProductService ProductService { get; private set; } = null!;
    public OrderService OrderService { get; private set; } = null!;

    // App
    public DialogService Dialog { get; private set; } = null!;
    public LoadingService Loading { get; private set; } = null!;
    public NavigationService Navigation { get; private set; } = null!;
    public NotificationService Notification { get; private set; } = null!;

    // Observer
    public NotificationPublisher NotificationPublisher { get; private set; } = null!; // Observer

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

        // Observer
        NotificationPublisher = new NotificationPublisher(); // Observer utama

        var logger = new SystemLogger(); // Logger
        NotificationPublisher.Subscribe(logger);

        NotificationPublisher.Subscribe(Notification); // Integrasi ke UI/Toast
    }
}