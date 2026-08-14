using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPilot.Api.Common;
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

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<TodoResponseDto>>>> GetAll()
    {
        return Ok(new ApiResponse<List<TodoResponseDto>>(true, await _service.GetAllAsync(), "All todos retrieved successfully.", null));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<TodoResponseDto>>> GetById(string id)
    {
        var todo = await _service.GetByIdAsync(id);

        if (todo == null)
            return NotFound(new ApiResponse<TodoResponseDto>(false, null, "Todo not found.", null));

        return Ok(new ApiResponse<TodoResponseDto>(true, todo, "Todo retrieved successfully.", null));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<TodoResponseDto>>> Create(CreateTodoDto dto)
    {
        try
        {
            var todoResponseDto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = todoResponseDto.Id }, new ApiResponse<TodoResponseDto>(true, todoResponseDto, "Todo created successfully.", null));
        }
        catch(Exception ex)
        {
            return BadRequest(new ApiResponse<TodoResponseDto>(false, null, ex.Message, new List<string> { ex.Message }));
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Update(string id, UpdateTodoDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new ApiResponse<TodoResponseDto>(true, null, "Todo updated successfully.", null));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(string id)
    {
        await _service.DeleteAsync(id);

        return Ok(new ApiResponse<TodoResponseDto>(true, null, "Todo deleted successfully.", null));
    }
}