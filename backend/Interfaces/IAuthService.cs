using TaskPilot.Api.DTOs.Auth;

namespace TaskPilot.Api.Interfaces;
public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshAsync(string refreshToken);
}