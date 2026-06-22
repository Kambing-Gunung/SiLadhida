namespace SiLadhida.App.Services.Core;

public class AuthSession
{
    public string? Token { get; private set; }
    public string? Role { get; private set; }

    public bool IsAuthenticated => !string.IsNullOrEmpty(Token);

    public void Set(string token, string role)
    {
        Token = token;
        Role = role;
    }

    public void Clear()
    {
        Token = null;
        Role = null;
    }
}