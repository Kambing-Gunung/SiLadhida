using Microsoft.AspNetCore.Mvc;
using SiLadhida.Application.Interfaces;
using SiLadhida.API.Common;
using SiLadhida.API.DTOs;
using SiLadhida.API.Auth;
using SiLadhida.Core.Entities;
using SiLadhida.Infrastructure.Persistence;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        ILogger<AuthController> logger,
        AppDbContext context)
    {
        _authService = authService;
        _logger = logger;
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        _logger.LogInformation("Login attempt for user {Username}", dto.Username);

        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);

        if (user is null)
        {
            _logger.LogWarning("Failed login attempt for user {Username}", dto.Username);

            return Unauthorized(ApiResponse<string>.ErrorResponse("Username atau password salah"));
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            _logger.LogWarning("Failed login attempt for user {Username}", dto.Username);

            return Unauthorized(ApiResponse<string>.ErrorResponse("Username atau password salah"));
        }

        _logger.LogInformation("User {Username} logged in successfully", dto.Username);

        var token = _authService.GenerateToken(new AppUser
        {
            Username = user.Username,
            Role = user.Role
        });

        return Ok(ApiResponse<object>.SuccessResponse(
            new { token, role = user.Role },
            "Login berhasil"
        ));
    }
}