namespace SiLadhida.App.Core.Auth;

public class AuthSession
{
    private static AuthSession? _instance;

    public static AuthSession Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AuthSession();

            return _instance;
        }
    }

    private AuthSession()
    {
    }

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