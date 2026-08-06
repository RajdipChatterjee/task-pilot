using todo_backend.Models;
using todo_backend.DTOs;

namespace todo_backend.Interfaces
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
