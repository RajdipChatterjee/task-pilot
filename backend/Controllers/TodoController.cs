using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskPilot.Api.Common;
using TaskPilot.Api.DTOs.Common;
using TaskPilot.Api.DTOs.Todo;
using TaskPilot.Api.Interfaces;

namespace TaskPilot.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService service)
    {
        _service = service;
    }

    //[HttpGet]
    //public async Task<ActionResult<ApiResponse<List<TodoResponseDto>>>> GetAll()
    //{
    //    var userId = User.FindFirst(ClaimTypes.Name)?.Value;

    //    var projectId = await _service.GetProjectIdByUserIdAsync(userId);
    //    return Ok(new ApiResponse<List<TodoResponseDto>>(true, await _service.GetAllAsync(projectId), "All todos retrieved successfully.", null));
    //}

    [HttpGet("{projectId}/tasks")]
    public async Task<ActionResult<ApiResponse<PagedResult<TodoResponseDto>>>> GetAll(string projectId, 
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? month = null,
        [FromQuery] int? year = null)
    {
        var tasks = await _service.GetAllAsync(projectId, pageNumber, pageSize, month, year);

        return Ok(new ApiResponse<PagedResult<TodoResponseDto>>(true, tasks, "All todos retrieved successfully.", null));
    }

    [HttpGet("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<ApiResponse<TodoResponseDto>>> GetById(string projectId, string taskId)
    {
        var todo = await _service.GetByIdAsync(taskId);

        if (todo == null)
            return NotFound(new ApiResponse<TodoResponseDto>(false, null, "Todo not found.", null));

        return Ok(new ApiResponse<TodoResponseDto>(true, todo, "Todo retrieved successfully.", null));
    }

    [HttpPost("{projectId}/tasks")]
    public async Task<ActionResult<ApiResponse<TodoResponseDto>>> Create(string projectId, CreateTodoDto dto)
    {
        try
        {
            var todoResponseDto = await _service.CreateAsync(projectId, dto);
            return CreatedAtAction(nameof(GetById), new { projectId = projectId, taskId = todoResponseDto.Id }, new ApiResponse<TodoResponseDto>(true, todoResponseDto, "Todo created successfully.", null));
        }
        catch(Exception ex)
        {
            return BadRequest(new ApiResponse<TodoResponseDto>(false, null, ex.Message, new List<string> { ex.Message }));
        }
    }

    [HttpPut("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<ApiResponse<string>>> Update(string projectId, string taskId, UpdateTodoDto dto)
    {
        await _service.UpdateAsync(taskId, dto);

        return Ok(new ApiResponse<string>(true, null, "Todo updated successfully.", null));
    }

    [HttpDelete("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(string projectId, string taskId)
    {
        await _service.DeleteAsync(taskId);

        return Ok(new ApiResponse<string>(true, null, "Todo deleted successfully.", null));
    }
}