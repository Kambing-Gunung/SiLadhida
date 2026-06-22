using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SiLadhida.Application.Interfaces;

namespace SiLadhida.API.Auth;

/// <summary>
/// Provides JWT token generation for authentication
/// </summary>
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateToken(AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        _logger.LogInformation("Generating JWT token for user {Username}", user.Username);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        _logger.LogInformation("JWT token generated successfully for user {Username}", user.Username);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}