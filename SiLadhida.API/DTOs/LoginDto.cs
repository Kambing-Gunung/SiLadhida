namespace SiLadhida.API.DTOs;

/// <summary>
/// Data transfer object for user login request
/// </summary>
public class LoginDto
{
    /// <summary>
    /// The username for authentication
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password for authentication
    /// </summary>
    public string Password { get; set; } = string.Empty;
}