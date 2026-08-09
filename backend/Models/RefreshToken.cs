namespace TaskPilot.Api.Models;

public class RefreshToken
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? DeviceName { get; set; }
    public string? IpAddress { get; set; }
}