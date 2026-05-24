namespace SiLadhida.API.Auth;

/// <summary>
/// Represents an application user for authentication
/// </summary>
public class AppUser
{
    /// <summary>
    /// The unique username for the user
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password for the user (should be hashed in production)
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// The role assigned to the user for authorization
    /// </summary>
    public string Role { get; set; } = string.Empty;
}