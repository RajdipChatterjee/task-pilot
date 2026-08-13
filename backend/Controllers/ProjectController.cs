using Microsoft.AspNetCore.Mvc;
using TaskPilot.Api.Common;
using TaskPilot.Api.DTOs.Project;
using TaskPilot.Api.Interfaces;

namespace TaskPilot.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("/[userId]/projects")]
    public async Task<ApiResponse<List<ProjectDetailsDto>?>> GetAllAsync(string userId)
    {
        try
        {
            // We'll get userId from claims
            var response = await _projectService.GetByUserIdAsync(userId);
            return new ApiResponse<List<ProjectDetailsDto>?>(true, response, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProjectDetailsDto>?>(false, null, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ApiResponse<string>> CreateProject(CreateProjectDto dto)
    {
        try
        {
            var response = await _projectService.CreateAsync(dto);
            return new ApiResponse<string>(true, "Project created successfully", null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>(false, null, ex.Message);
        }
    }

}
