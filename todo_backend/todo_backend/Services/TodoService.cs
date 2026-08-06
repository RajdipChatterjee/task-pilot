using todo_backend.DTOs;
using todo_backend.Interfaces;
using todo_backend.Models;

namespace todo_backend.Services;

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
            IsCompleted = t.IsCompleted
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
            IsCompleted = todo.IsCompleted
        };
    }

    public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
    {
        var todo = new Todo
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false
        };

        await _repository.CreateAsync(todo);

        return new TodoResponseDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };
    }

    public async Task UpdateAsync(string id, UpdateTodoDto dto)
    {
        var todo = new Todo
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted
        };

        await _repository.UpdateAsync(id, todo);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}