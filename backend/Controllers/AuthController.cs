using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPilot.Api.Common;
using TaskPilot.Api.DTOs.Auth;
using TaskPilot.Api.Interfaces;

namespace TaskPilot.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
            Username = User.Identity?.Name
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<string>>> LoginUser(LoginDto dto)
    {

        try
        {
            var result = await _authService.LoginAsync(dto);

            Response.Cookies.Append(
            "accessToken",
            result.AccessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            });

            Response.Cookies.Append(
                "refreshToken",
                result.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

            return Ok(new ApiResponse<string>(
                true,
                "User logged in",
                "User logged in"
            ));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<string>>> CreateUser(RegisterDto dto)
    {
        try
        {
            await _authService.RegisterAsync(dto);
            return Ok(new ApiResponse<string>(true, "User registered successfully", "Registration completed"));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<ApiResponse<string>>> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized("Refresh token is missing.");

        var result = await _authService.RefreshAsync(refreshToken);

        Response.Cookies.Append(
            "accessToken",
            result.AccessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            });

        return Ok(new ApiResponse<string>(
            true,
            "Token refreshed successfully",
            "Token refreshed successfully"
        ));
    }
}