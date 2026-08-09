using TaskPilot.Api.Models;

namespace TaskPilot.Api.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
}