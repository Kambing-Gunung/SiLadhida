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
    private AuthSession? _session;

    // SINGLETON INSTANCE
    private static ApiClient? _instance;

    public static ApiClient Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ApiClient();

            return _instance;
        }
    }

    // PRIVATE CONSTRUCTOR
    private ApiClient()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5135/")
        };
    }

    // SET SESSION (inject manual)
    public void SetSession(AuthSession session)
    {
        _session = session;
    }

    private void AttachToken()
    {
        _http.DefaultRequestHeaders.Authorization = null;

        if (_session != null && _session.IsAuthenticated)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _session.Token);
        }
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        AttachToken();

        var response = await _http.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        HandleError(response, json);

        return Deserialize<T>(json);
    }

    public async Task<T?> PostAsync<T>(string url, object data)
    {
        AttachToken();

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        HandleError(response, result);

        return Deserialize<T>(result);
    }

    public async Task<T?> PutAsync<T>(string url, object? data = null)
    {
        AttachToken();

        HttpContent? content = null;

        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _http.PutAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        HandleError(response, result);

        return Deserialize<T>(result);
    }

    public async Task<T?> PatchAsync<T>(string url, object? data = null)
    {
        AttachToken();

        HttpContent? content = null;

        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = content
        };

        var response = await _http.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        HandleError(response, result);

        return Deserialize<T>(result);
    }

    public async Task DeleteAsync(string url)
    {
        AttachToken();

        var response = await _http.DeleteAsync(url);
        var result = await response.Content.ReadAsStringAsync();

        HandleError(response, result);
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

        if (!string.IsNullOrWhiteSpace(content))
        {
            message += $": {content}";
        }

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
            // ignore
        }

        throw new Exception(message);
    }
}