using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiLadhida.App.Services;

public class ApiService
{
    protected readonly HttpClient HttpClient;

    protected readonly JsonSerializerOptions JsonOptions =
        new()
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

    public ApiService()
    {
        HttpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5135/")
        };

        // AttachToken();
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

    protected async Task<T?> GetAsync<T>(string url)
    {
        AttachToken(); // 🔥 pindah ke sini

        var response = await HttpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"GET {url} FAILED: {response.StatusCode} - {error}");
        }

        return await response.Content.ReadFromJsonAsync<T>(JsonOptions);
    }

    protected async Task<HttpResponseMessage> PostAsync<T>(
    string url,
    T data)
    {
        AttachToken(); // 🔥

        var response = await HttpClient.PostAsJsonAsync(
            url,
            data,
            JsonOptions
        );

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"POST {url} FAILED: {response.StatusCode} - {error}");
        }

        return response;
    }

    protected async Task<HttpResponseMessage> PutAsync<T>(
    string url,
    T data)
    {
        AttachToken(); // 🔥

        var response = await HttpClient.PutAsJsonAsync(
            url,
            data,
            JsonOptions
        );

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"PUT {url} FAILED: {response.StatusCode} - {error}");
        }

        return response;
    }

    protected async Task<HttpResponseMessage> PutAsync(string url)
    {
        AttachToken(); // 🔥

        var response = await HttpClient.PutAsync(url, null);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"PUT {url} FAILED: {response.StatusCode} - {error}");
        }

        return response;
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        AttachToken(); // 🔥

        var response = await HttpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"DELETE {url} FAILED: {response.StatusCode} - {error}");
        }

        return response;
    }
}