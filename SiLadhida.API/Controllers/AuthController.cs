using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.Auth;
using SiLadhida.API.Common;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        // dummy user sementara
        if (dto.Username != "admin" ||
            dto.Password != "admin123")
        {
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