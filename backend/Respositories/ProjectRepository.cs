using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskPilot.Api.Configurations;
using TaskPilot.Api.DTOs.Project;
using TaskPilot.Api.Interfaces;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Respositories;

public class ProjectRepository : IProjectRepository
{
    private readonly IMongoCollection<Project> _projects;
    public ProjectRepository(IOptions<MongoDbSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

        _projects = mongoDatabase.GetCollection<Project>(
            options.Value.ProjectCollection);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        await _projects.InsertOneAsync(project);
        return project;
    }
    public async Task<ProjectDetailsDto?> GetByIdAsync(string id)
    {
        //var project = await _projects.Find(project => id == project.Id).FirstOrDefaultAsync();

        var pipeline = new[]
        {
            new BsonDocument("$match", new BsonDocument("_id", ObjectId.Parse(id))),
            new BsonDocument("$lookup",
                new BsonDocument
                {
                    {"from", "Todos" },
                    {"localField", "_id" },
                    {"foreignField", "projectId" },
                    {"as", "tasks" }
                }
            ),
            new BsonDocument("$project",
                new BsonDocument
                {
                    {"_id", 1 },
                    {"name", 1 },
                    {"description", 1 },
                    {"createdBy", 1},
                    {"taskCount", new BsonDocument("$size", "$tasks") },
                    {"completedTaskCount", new BsonDocument("$size", new BsonDocument("$filter", new BsonDocument
                            {
                                { "input", "$tasks" },
                                { "as", "task" },
                                {"cond", new BsonDocument("$eq", new BsonArray{"$$task.status", 1})}
                            }
                        )
                    )},
                    {"pendingTaskCount",new BsonDocument("$size", new BsonDocument("$filter", new BsonDocument
                            {
                                { "input", "$tasks" },
                                { "as", "task" },
                                { "cond", new BsonDocument("$eq", new BsonArray {"$$task.status", 0}) }
                            }
                        )
                    )},
                    {"createdAt", 1 }
                }
            )
        };

        var project = await _projects.Aggregate<ProjectDetailsDto>(pipeline).FirstOrDefaultAsync();

        return project;
    }
    public async Task<List<ProjectDetailsDto>> GetByUserIdAsync(string userId, int pageNumber, int pageSize, int? month, int? year)
    {
        //var projects = await _projects.Find(project => project.CreatedBy == userId).ToListAsync();

        var match = new BsonDocument {
            { "createdBy", ObjectId.Parse(userId) }
        };

        if (month.HasValue && year.HasValue)
        {
            var startDate = new DateTime(year.Value, month.Value, 1);
            var endDate = startDate.AddMonths(1);

            match.Add( "createdAt", new BsonDocument
            {
                { "$gte", startDate },
                { "$lt", endDate }
            });
        }

        var pipeline = new[]
        {
            //new BsonDocument("$match", new BsonDocument("createdBy", ObjectId.Parse(userId))),

            new BsonDocument("$match", match),

            new BsonDocument("$lookup",
                new BsonDocument
                {
                    {"from", "Todos" },
                    {"localField", "_id" },
                    {"foreignField", "projectId" },
                    {"as", "tasks" }
                }
            ),

            new BsonDocument("$project",
                new BsonDocument
                {
                    {"_id", 1 },
                    {"name", 1 },
                    {"description", 1 },
                    {"createdBy", 1 },
                    {"taskCount", new BsonDocument("$size", "$tasks") },
                    {"completedTaskCount", new BsonDocument("$size", new BsonDocument("$filter", new BsonDocument
                            {
                                { "input", "$tasks" },
                                { "as", "task" },
                                {"cond", new BsonDocument("$eq", new BsonArray{"$$task.status", 1})}
                            }
                        )
                    )},
                    {"pendingTaskCount",new BsonDocument("$size", new BsonDocument("$filter", new BsonDocument
                            {
                                { "input", "$tasks" },
                                { "as", "task" },
                                { "cond", new BsonDocument("$eq", new BsonArray {"$$task.status", 0}) }
                            }
                        )
                    )},
                    {"createdAt", 1 }
                }
            ),

            new BsonDocument("$sort", new BsonDocument("createdAt", -1)),
            new BsonDocument("$skip", (pageNumber - 1) * pageSize),
            new BsonDocument("$limit", pageSize),
        };

        var projects = await _projects.Aggregate<ProjectDetailsDto>(pipeline).ToListAsync();

        return projects;
    }

    public async Task DeleteAsync(string id)
    {
        await _projects.DeleteOneAsync(project => project.Id == id);
    }

    public async Task UpdateAsync(Project project)
    {
        var filter = Builders<Project>.Filter.Eq(p => p.Id, project.Id);

        var update = Builders<Project>.Update
            .Set(p => p.Name, project.Name)
            .Set(p => p.Description, project.Description)
            .Set(p => p.UpdatedAt, DateTime.UtcNow);

        await _projects.UpdateOneAsync(filter, update);
    }

}
