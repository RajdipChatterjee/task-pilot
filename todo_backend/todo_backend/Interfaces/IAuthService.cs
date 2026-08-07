using todo_backend.DTOs.Auth;

namespace todo_backend.Interfaces;
public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}