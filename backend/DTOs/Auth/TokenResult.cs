namespace TaskPilot.Api.DTOs.Auth;

public class TokenResult
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
