using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiLadhida.App.Services.Core;

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
        // Reset dulu
        _http.DefaultRequestHeaders.Authorization = null;

        // Pasang token kalau ada
        if (_session.IsAuthenticated)
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

        return Deserialize<T>(json);
    }

    public async Task<T?> PostAsync<T>(string url, object data)
    {
        AttachToken();

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

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

        return Deserialize<T>(result);
    }

    public async Task DeleteAsync(string url)
    {
        AttachToken();
        await _http.DeleteAsync(url);
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
}