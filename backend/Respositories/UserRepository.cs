using Microsoft.Extensions.Options;
using TaskPilot.Api.Models;
using TaskPilot.Api.Configurations;
using TaskPilot.Api.Interfaces;
using MongoDB.Driver;

namespace TaskPilot.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IOptions<MongoDbSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

        _users = mongoDatabase.GetCollection<User>(
            options.Value.UserCollection);
    }

    public async Task<User?> GetByUsernameOrEmailAsync(
        string usernameOrEmail)
    {
        return await _users
            .Find(x =>
                x.Username == usernameOrEmail ||
                x.Email == usernameOrEmail)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _users
            .Find(x => x.Username == username)
            .AnyAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _users
            .Find(x => x.Email == email)
            .AnyAsync();
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _users
            .Find(x => x.RefreshTokens.Any(r => r.Token == refreshToken))
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _users.ReplaceOneAsync(
            x => x.Id == user.Id,
            user);
    }
}