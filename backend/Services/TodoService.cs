using TaskPilot.Api.DTOs.Todo;
using TaskPilot.Api.Interfaces;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TodoResponseDto>> GetAllAsync()
    {
        var todos = await _repository.GetAllAsync();

        return todos.Select(t => new TodoResponseDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status
        }).ToList();
    }

    public async Task<TodoResponseDto?> GetByIdAsync(string id)
    {
        var todo = await _repository.GetByIdAsync(id);

        if (todo == null)
            return null;

        return new TodoResponseDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            Status = todo.Status
        };
    }

    public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
    {
        var todo = new Todo
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status
        };

        await _repository.CreateAsync(todo);

        return new TodoResponseDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            Status = todo.Status
        };
    }

    public async Task UpdateAsync(string id, UpdateTodoDto dto)
    {
        var todo = new Todo
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status
        };

        await _repository.UpdateAsync(id, todo);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}