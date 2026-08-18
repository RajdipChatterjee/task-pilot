using TaskPilot.Api.DTOs.Project;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Interfaces;
public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project);
    Task<ProjectDetailsDto?> GetByIdAsync(string id);
    Task<List<ProjectDetailsDto>> GetByUserIdAsync(string userId, int pageNumber, int pageSize, int? month, int? year);
    Task UpdateAsync(Project project);  
    Task DeleteAsync(string id);
}   