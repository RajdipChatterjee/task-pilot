using TaskPilot.Api.Models;

namespace TaskPilot.Api.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
}