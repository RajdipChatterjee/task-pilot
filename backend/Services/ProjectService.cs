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
    public async Task<Project> CreateAsync(Project project)
    {
        return await _projectRepository.CreateAsync(project);
    }
    public async Task<Project?> GetByIdAsync(string id)
    {
        return await _projectRepository.GetByIdAsync(id);
    }
    
    public async Task<List<Project>> GetByUserIdAsync(string userId)
    {
        return await _projectRepository.GetByUserIdAsync(userId);
    }

    public async Task DeleteAsync(string id)
    {
        await _projectRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(Project project)
    {
        await _projectRepository.UpdateAsync(project);
    }
}
