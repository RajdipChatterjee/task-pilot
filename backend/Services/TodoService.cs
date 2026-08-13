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
            ProjectId = t.ProjectId,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            TaskDate = t.TaskDate,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
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
            ProjectId = todo.ProjectId,
            Title = todo.Title,
            Description = todo.Description,
            Status = todo.Status,
            TaskDate = todo.TaskDate,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
    {
        var todo = new Todo
        {
            ProjectId = dto.ProjectId,
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            TaskDate = dto.TaskDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(todo);

        return new TodoResponseDto
        {
            Id = todo.Id,
            ProjectId = todo.ProjectId,
            Title = todo.Title,
            Description = todo.Description,
            Status = todo.Status,
            TaskDate = todo.TaskDate,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public async Task UpdateAsync(string id, UpdateTodoDto dto)
    {
        var todo = new Todo
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            TaskDate = dto.TaskDate
        };

        await _repository.UpdateAsync(id, todo);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}