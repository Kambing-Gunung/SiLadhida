using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.Auth;
using SiLadhida.API.Common;
using SiLadhida.API.DTOs;
using SiLadhida.API.Services.Interfaces;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        if (dto == null)
        {
            return BadRequest(
                ApiResponse<string>.ErrorResponse(
                    "Username dan password tidak boleh kosong"
                )
            );
        }

        _logger.LogInformation("Login attempt for user {Username}", dto.Username);

        // TODO: Replace with proper user authentication from database
        if (dto.Username != "admin" || dto.Password != "admin123")
        {
            _logger.LogWarning("Failed login attempt for user {Username}", dto.Username);

            return Unauthorized(
                ApiResponse<string>.ErrorResponse(
                    "Username atau password salah"
                )
            );
        }

        var user = new AppUser
        {
            Username = dto.Username,
            Role = "Admin"
        };

        var token = _authService.GenerateToken(user);

        _logger.LogInformation("User {Username} logged in successfully", dto.Username);

        return Ok(
            ApiResponse<object>.SuccessResponse(
                new
                {
                    token,
                    role = user.Role
                },
                "Login berhasil"
            )
        );
    }
}