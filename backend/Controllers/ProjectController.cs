using Microsoft.AspNetCore.Mvc;
using TaskPilot.Api.Common;
using TaskPilot.Api.DTOs.Project;
using TaskPilot.Api.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TaskPilot.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ProjectDetailsDto>>>> GetAllAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? month = null, [FromQuery] int? year = null)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new ApiResponse<List<ProjectDetailsDto>>(
                    false, null, "User not authenticated"));

            var response = await _projectService.GetByUserIdAsync(userId, pageNumber, pageSize, month, year);

            return Ok(new ApiResponse<List<ProjectDetailsDto>?>(true, response, null));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<List<ProjectDetailsDto>?>(false, null, ex.Message));
        }
    }

    [HttpGet("{id}", Name = "GetProjectById")]
    public async Task<ActionResult<ApiResponse<ProjectDetailsDto>>> GetAsync(string id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new ApiResponse<ProjectDetailsDto?>(
                    false, null, "User not authenticated"));

            var response = await _projectService.GetByIdAsync(id, userId);

            if (response == null)
                return NotFound(new ApiResponse<ProjectDetailsDto?>(false, null, "Project not found"));

            return Ok(new ApiResponse<ProjectDetailsDto?>(true, response, null));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<ProjectDetailsDto>(false, null, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProjectDetailsDto>>> CreateProject(CreateProjectDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new ApiResponse<ProjectDetailsDto>(false, null, "User not authenticated"));

            var response = await _projectService.CreateAsync(dto, userId);
            return CreatedAtRoute("GetProjectById", new { id = response.Id }, new ApiResponse<ProjectDetailsDto>(true, response, null));
        }
        catch (Exception ex)
        {
            return new ApiResponse<ProjectDetailsDto>(false, null, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateProject(string id, UpdateProjectDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new ApiResponse<ProjectDetailsDto>(false, null, "User not authenticated"));

            await _projectService.UpdateAsync(id, dto, userId);

            return Ok(new ApiResponse<string>(
                true,
                "Project updated successfully",
                null));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<string>(
                false,
                null,
                ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteProject(string id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new ApiResponse<ProjectDetailsDto>(false, null, "User not authenticated"));

            await _projectService.DeleteAsync(id, userId);

            return Ok(new ApiResponse<string>(
                true,
                "Project deleted successfully",
                null));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<string>(
                false,
                null,
                ex.Message));
        }
    }

}
