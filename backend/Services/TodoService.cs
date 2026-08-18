using TaskPilot.Api.DTOs.Common;
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

    public async Task<PagedResult<TodoResponseDto>> GetAllAsync(
    string projectId,
    int pageNumber,
    int pageSize,
    int? month,
    int? year)
    {
        var result = await _repository.GetAllAsync(
            projectId,
            pageNumber,
            pageSize,
            month,
            year);

        return new PagedResult<TodoResponseDto>
        {
            Items = result.Items.Select(t => new TodoResponseDto
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                TaskDate = t.TaskDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList(),

            TotalItems = result.TotalItems,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages
        };
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

    public async Task<TodoResponseDto> CreateAsync(string projectId, CreateTodoDto dto)
    {
        var todo = new Todo
        {
            ProjectId = projectId,
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
        var todo = await _repository.GetByIdAsync(id);

        if (todo == null)
            throw new KeyNotFoundException("Todo not found.");

        todo.Title = dto.Title;
        todo.Description = dto.Description;
        todo.Status = dto.Status;
        todo.TaskDate = dto.TaskDate;
        todo.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(id, todo);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}