using System.Threading.Tasks;
using SiLadhida.App.Core.Api;
using SiLadhida.App.Core.Auth;
using SiLadhida.App.Core.Models;

namespace SiLadhida.App.Services.Api;

public class AuthService
{
    private readonly ApiClient _client = ApiClient.Instance;
    private readonly AuthSession _session;

    public AuthService(ApiClient client, AuthSession session)
    {
        _client = client;
        _session = session;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var response = await _client.PostAsync<ApiResponse<LoginResponse>>(
            "api/auth/login",
            new { username, password });


        if (response?.Data == null)
            return false;

        _session.Set(response.Data.Token, response.Data.Role);

        return true;
    }

    public void Logout()
    {
        _session.Clear();
    }
}