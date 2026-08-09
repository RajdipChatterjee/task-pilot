namespace TaskPilot.Api.Models;

public class ExternalLogin
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public string ProviderUserId { get; set; } = null!;
}