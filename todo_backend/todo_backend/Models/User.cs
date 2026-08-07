namespace todo_backend.Models;

public class User
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "User";
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}