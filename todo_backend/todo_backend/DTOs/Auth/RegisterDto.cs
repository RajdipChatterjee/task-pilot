using todo_backend.Enums;

namespace todo_backend.DTOs.Auth;

public class RegisterDto
{
    public string Username { get; set; } = null!;
    public string Email {  get; set; } = null!;
    public string Password { get; set; } = null!;
}