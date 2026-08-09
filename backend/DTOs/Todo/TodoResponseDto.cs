using TaskPilot.Api.Enums;

namespace TaskPilot.Api.DTOs.Todo;

public class TodoResponseDto
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TodoStatus Status { get; set; }
}