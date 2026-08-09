using TaskPilot.Api.Models;
using TaskPilot.Api.DTOs.Todo;

namespace TaskPilot.Api.Interfaces
{
    public interface ITodoService
    {
        Task<List<TodoResponseDto>> GetAllAsync();

        Task<TodoResponseDto?> GetByIdAsync(string id);

        Task<TodoResponseDto> CreateAsync(CreateTodoDto todo);

        Task UpdateAsync(string id, UpdateTodoDto todo);

        Task DeleteAsync(string id);
    }
}
