using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskPilot.Api.Configurations;
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
    public async Task<Project?> GetByIdAsync(string id)
    {
        var project = await _projects.Find(project => id == project.Id).FirstOrDefaultAsync();

        return project;
    }
    public async Task<List<Project>> GetByUserIdAsync(string userId)
    {
        var projects = await _projects.Find(project => project.CreatedBy == userId).ToListAsync();

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
