using TaskPilot.Api.DTOs.Project;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Interfaces;

public interface IProjectService
{
    Task<Project> CreateAsync(CreateProjectDto project);
    Task<Project?> GetByIdAsync(string id);
    Task<List<ProjectDetailsDto?>> GetByUserIdAsync(string userId);
    Task UpdateAsync(CreateProjectDto project);
    Task DeleteAsync(string id);
}