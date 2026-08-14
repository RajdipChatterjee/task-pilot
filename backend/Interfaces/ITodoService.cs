using TaskPilot.Api.DTOs.Todo;

namespace TaskPilot.Api.Interfaces
{
    public interface ITodoService
    {
        Task<List<TodoResponseDto>> GetAllAsync(string projectId);

        Task<TodoResponseDto?> GetByIdAsync(string id);

        Task<TodoResponseDto> CreateAsync(string projectId, CreateTodoDto todo);

        Task UpdateAsync(string id, UpdateTodoDto todo);

        Task DeleteAsync(string id);
    }
}
