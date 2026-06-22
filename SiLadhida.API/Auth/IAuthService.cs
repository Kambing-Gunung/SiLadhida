namespace SiLadhida.API.Auth;

/// <summary>
/// Provides authentication and token generation services
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Generates a JWT token for the given user
    /// </summary>
    string GenerateToken(AppUser user);
}
