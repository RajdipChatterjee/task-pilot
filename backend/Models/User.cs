namespace TaskPilot.Api.Models;

public class User
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PasswordHash { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public List<ExternalLogin> ExternalLogins { get; set; } = [];
}