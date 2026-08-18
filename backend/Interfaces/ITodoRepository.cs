using TaskPilot.Api.DTOs.Common;
using TaskPilot.Api.DTOs.Todo;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Interfaces
{

    public interface ITodoRepository
    {
        Task<PagedResult<TodoResponseDto>> GetAllAsync(string projectId, int pageNumber, int pageSize, int? month, int? year);

        Task<Todo?> GetByIdAsync(string id);

        Task CreateAsync(Todo todo);

        Task UpdateAsync(string id, Todo todo);

        Task DeleteAsync(string id);
    }

}