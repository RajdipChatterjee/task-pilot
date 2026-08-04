using Microsoft.AspNetCore.Mvc;
using todo_backend.DTOs;
using todo_backend.Interfaces;

namespace todo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoResponseDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoResponseDto>> GetById(string id)
    {
        var todo = await _service.GetByIdAsync(id);

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoDto dto)
    {
        try
        {
            await _service.CreateAsync(dto);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateTodoDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}