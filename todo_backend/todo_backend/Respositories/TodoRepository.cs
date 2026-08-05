using Microsoft.Extensions.Options;
using todo_backend.Models;
using todo_backend.Configurations;
using todo_backend.Interfaces;
using MongoDB.Driver;

namespace todo_backend.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IMongoCollection<Todo> _todos;

    public TodoRepository(IOptions<MongoDbSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        _todos = mongoDatabase.GetCollection<Todo>(options.Value.TodoCollection);
    }

    public async Task<List<Todo>> GetAllAsync()
    {
        return await _todos.Find(_ => true).ToListAsync();
    }

    public async Task<Todo> GetByIdAsync(string id)
    {
        return await _todos.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Todo todo)
    {
        await _todos.InsertOneAsync(todo);
    }

    public async Task UpdateAsync(string id, Todo todo)
    {
        await _todos.ReplaceOneAsync(x => x.Id == id, todo);
    }

    public async Task DeleteAsync(string id)
    {
        await _todos.DeleteOneAsync(x => x.Id == id);
    }
}