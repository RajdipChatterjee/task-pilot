using Microsoft.Extensions.Options;
using TaskPilot.Api.Models;
using TaskPilot.Api.Configurations;
using TaskPilot.Api.Interfaces;
using MongoDB.Driver;

namespace TaskPilot.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IMongoCollection<Todo> _todos;

    public TodoRepository(IOptions<MongoDbSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        _todos = mongoDatabase.GetCollection<Todo>(options.Value.TodoCollection);
    }

    public async Task<List<Todo>> GetAllAsync(string projectId)
    {
        return await _todos.Find(t => t.ProjectId == projectId).ToListAsync();
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
        var filter = Builders<Todo>.Filter.Eq(
            x => x.Id,
            id);

        var update = Builders<Todo>.Update
            .Set(x => x.Title, todo.Title)
            .Set(x => x.Description, todo.Description)
            .Set(x => x.Status, todo.Status)
            .Set(x => x.TaskDate, todo.TaskDate)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _todos.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAsync(string id)
    {
        await _todos.DeleteOneAsync(x => x.Id == id);
    }
}