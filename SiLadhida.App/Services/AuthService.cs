using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiLadhida.App.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public static string Token = "";

    public AuthService()
    {
        _httpClient = new HttpClient();

        _httpClient.BaseAddress =
            new Uri("http://localhost:5135/");
    }

    public async Task<bool> LoginAsync(
        string username,
        string password)
    {
        var body = new
        {
            username,
            password
        };

        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        var response =
            await _httpClient.PostAsync(
                "api/auth/login",
                content
            );

        if (!response.IsSuccessStatusCode)
            return false;

        var responseJson =
            await response.Content.ReadAsStringAsync();

        using var doc =
            JsonDocument.Parse(responseJson);

        Token =
            doc.RootElement
                .GetProperty("data")
                .GetProperty("token")
                .GetString() ?? "";
        Console.WriteLine(Token);
        return true;
    }

    public static void AttachToken(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                Token
            );
    }

    // get token untuk ditampilkan di UI
    // public string GetToken()
    // {
    //     return Token;
    // }
}