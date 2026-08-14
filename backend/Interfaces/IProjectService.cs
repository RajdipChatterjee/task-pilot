using TaskPilot.Api.DTOs.Project;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Interfaces;

public interface IProjectService
{
    Task<ProjectDetailsDto> CreateAsync(CreateProjectDto project, string userId);
    Task<ProjectDetailsDto?> GetByIdAsync(string id, string userId);
    Task<List<ProjectDetailsDto?>> GetByUserIdAsync(string userId);
    Task UpdateAsync(string id, UpdateProjectDto project, string userId);
    Task DeleteAsync(string id, string userId);
}