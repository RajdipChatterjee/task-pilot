using TaskPilot.Api.Models;

namespace TaskPilot.Api.Interfaces;
public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project);
    Task<Project?> GetByIdAsync(string id);
    Task<List<Project>> GetByUserIdAsync(string userId);
    Task UpdateAsync(Project project);
    Task DeleteAsync(string id);
}