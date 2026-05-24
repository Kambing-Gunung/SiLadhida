using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SiLadhida.App.Services;

public class ApiService
{
    protected readonly HttpClient HttpClient;

    public ApiService()
    {
        HttpClient = new HttpClient();

        HttpClient.BaseAddress =
            new Uri("http://localhost:5135/");

        AttachToken();
    }

    private void AttachToken()
    {
        if (!string.IsNullOrWhiteSpace(AuthService.Token))
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    AuthService.Token
                );
        }
    }
}