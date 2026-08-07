using Microsoft.Extensions.Options;
using todo_backend.Models;
using todo_backend.Configurations;
using todo_backend.Interfaces;
using MongoDB.Driver;

namespace todo_backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IOptions<MongoDbSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        _users = mongoDatabase.GetCollection<User>(options.Value.UserCollection);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _users.Find(x => x.Username == username).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _users.ReplaceOneAsync(x => x.Id == user.Id, user);
    }
}