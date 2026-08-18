using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskPilot.Api.Configurations;
using TaskPilot.Api.DTOs.Common;
using TaskPilot.Api.DTOs.Todo;
using TaskPilot.Api.Interfaces;
using TaskPilot.Api.Models;

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

    public async Task<PagedResult<TodoResponseDto>> GetAllAsync(
        string projectId,
        int pageNumber,
        int pageSize,
        int? month,
        int? year)
    {
        var match = new BsonDocument
    {
        { "projectId", ObjectId.Parse(projectId) }
    };

        if (month.HasValue && year.HasValue)
        {
            var startDate = new DateTime(year.Value, month.Value, 1);
            var endDate = startDate.AddMonths(1);

            match.Add("taskDate", new BsonDocument
        {
            { "$gte", startDate },
            { "$lt", endDate }
        });
        }

        var pipeline = new[]
        {
        new BsonDocument("$match", match),

        new BsonDocument("$facet", new BsonDocument
        {
            {
                "items", new BsonArray
                {
                    new BsonDocument("$sort",
                        new BsonDocument("taskDate", -1)),

                    new BsonDocument("$skip",
                        (pageNumber - 1) * pageSize),

                    new BsonDocument("$limit", pageSize)
                }
            },
            {
                "metadata", new BsonArray
                {
                    new BsonDocument("$count", "totalItems")
                }
            }
        })
    };

        var result = await _todos
            .Aggregate<BsonDocument>(pipeline)
            .FirstOrDefaultAsync();

        var items = result["items"]
            .AsBsonArray
            .Select(x => MongoDB.Bson.Serialization.BsonSerializer
                .Deserialize<TodoResponseDto>(x.AsBsonDocument))
            .ToList();

        var totalItems = result["metadata"]
            .AsBsonArray
            .FirstOrDefault()?["totalItems"]
            .AsInt32 ?? 0;

        return new PagedResult<TodoResponseDto>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(
                (double)totalItems / pageSize)
        };
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