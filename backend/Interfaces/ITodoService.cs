using TaskPilot.Api.DTOs.Common;
using TaskPilot.Api.DTOs.Todo;

namespace TaskPilot.Api.Interfaces
{
    public interface ITodoService
    {
        Task<PagedResult<TodoResponseDto>> GetAllAsync(string projectId, int pageNumber, int pageSize, int? month, int? year);

        Task<TodoResponseDto?> GetByIdAsync(string id);

        Task<TodoResponseDto> CreateAsync(string projectId, CreateTodoDto todo);

        Task UpdateAsync(string id, UpdateTodoDto todo);

        Task DeleteAsync(string id);
    }
}
