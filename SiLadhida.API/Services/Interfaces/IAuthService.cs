using SiLadhida.API.Auth;

namespace SiLadhida.API.Services.Interfaces;

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
