using TaskPilot.Api.DTOs.Project;
using TaskPilot.Api.Interfaces;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDetailsDto?> CreateAsync(CreateProjectDto dto, string userId)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _projectRepository.CreateAsync(project);

        return MapToDto(project);
    }

    public async Task<ProjectDetailsDto?> GetByIdAsync(string id, string userId)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            return null;

        if (project.CreatedBy != userId)
            return null;

        return project;
    }

    public async Task<List<ProjectDetailsDto>> GetByUserIdAsync(string userId, int pageNumber, int pageSize, int? month, int? year)
    {
        var projects = await _projectRepository.GetByUserIdAsync(userId, pageNumber, pageSize, month, year);

        return projects;
    }

    public async Task UpdateAsync(string id, UpdateProjectDto dto, string userId)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            throw new KeyNotFoundException("Project not found.");

        if (project.CreatedBy != userId)
            throw new UnauthorizedAccessException(
                "You do not have access to this project.");

        var pr = DtoToModel(project);

        await _projectRepository.UpdateAsync(pr);
    }

    public async Task DeleteAsync(string id, string userId)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            throw new KeyNotFoundException("Project not found.");

        if (project.CreatedBy != userId)
            throw new UnauthorizedAccessException(
                "You do not have access to this project.");

        await _projectRepository.DeleteAsync(id);
    }

    private static ProjectDetailsDto MapToDto(Project project)
    {
        return new ProjectDetailsDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedBy = project.CreatedBy,
            CreatedAt = project.CreatedAt,
            TaskCount = 0
        };
    }

    private static Project DtoToModel(ProjectDetailsDto dto)
    {
        return new Project
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            UpdatedAt = DateTime.UtcNow
        };
    }
}