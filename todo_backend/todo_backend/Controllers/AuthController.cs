using Microsoft.AspNetCore.Mvc;
using todo_backend.Common;
using todo_backend.DTOs.Auth;
using todo_backend.Interfaces;

namespace todo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<string>>> Register(RegisterDto dto)
    {
        try
        {
            await _service.RegisterAsync(dto);

            return Ok(new ApiResponse<string>(
                success: true,
                data: "User registered successfully",
                message: "Registration completed"
            ));
        }
        catch (Exception ex)
        {
            return Conflict(new ApiResponse<string>(
                success: false,
                data: null,
                message: ex.Message
            ));
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(LoginDto dto)
    {
        try
        {
            var response = await _service.LoginAsync(dto);

            Response.Cookies.Append(
                "refreshToken",
                response.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // true when using HTTPS in production
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

            // Hide the refresh token from the JSON response
            response.RefreshToken = null;

            return Ok(new ApiResponse<AuthResponseDto>(
                success: true,
                data: response,
                message: "Login successful"
            ));
        }
        catch (Exception ex)
        {
            return Unauthorized(new ApiResponse<AuthResponseDto>(
                success: false,
                data: null,
                message: ex.Message
            ));
        }
    }
}