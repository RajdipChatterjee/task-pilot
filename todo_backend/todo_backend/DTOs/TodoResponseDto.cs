using todo_backend.Enums;

namespace todo_backend.DTOs;

public class TodoResponseDto
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TodoStatus Status { get; set; }
}