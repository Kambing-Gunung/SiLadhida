using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SiLadhida.App.Core.Auth;
using SiLadhida.App.Core.Models;

namespace SiLadhida.App.Core.Api;

public class ApiClient
{
    private readonly HttpClient _http;
    private readonly AuthSession _session;

    public ApiClient(AuthSession session)
    {
        _session = session;

        _http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5135/")
        };
    }

    private void AttachToken()
    {
        _http.DefaultRequestHeaders.Authorization = null;

        if (_session.IsAuthenticated)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _session.Token);
        }
    }

    // 🔥 CORE METHOD (INI KUNCI)
    private async Task<T?> SendAsync<T>(HttpMethod method, string url, object? data = null)
    {
        AttachToken();

        HttpContent? content = null;

        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var request = new HttpRequestMessage(method, url)
        {
            Content = content
        };

        var response = await _http.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        HandleError(response, result);

        return Deserialize<T>(result);
    }

    // 🔥 PUBLIC METHODS
    public Task<T?> GetAsync<T>(string url) 
        => SendAsync<T>(HttpMethod.Get, url);

    public Task<T?> PostAsync<T>(string url, object data)
        => SendAsync<T>(HttpMethod.Post, url, data);

    public Task<T?> PutAsync<T>(string url, object? data = null)
        => SendAsync<T>(HttpMethod.Put, url, data);

    public Task<T?> PatchAsync<T>(string url, object data)
        => SendAsync<T>(HttpMethod.Patch, url, data);

    public async Task DeleteAsync(string url)
    {
        await SendAsync<object>(HttpMethod.Delete, url);
    }

    private static T? Deserialize<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonSerializer.Deserialize<T>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }

    private static void HandleError(HttpResponseMessage response, string content)
    {
        if (response.IsSuccessStatusCode)
            return;

        var message = $"API Error ({(int)response.StatusCode})";

        try
        {
            var api = JsonSerializer.Deserialize<ApiResponse<object>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (api != null && !string.IsNullOrWhiteSpace(api.Message))
            {
                message = api.Message;
            }
        }
        catch
        {
            if (!string.IsNullOrWhiteSpace(content))
                message += $": {content}";
        }

        throw new Exception(message);
    }
}