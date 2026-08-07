using System.ComponentModel.DataAnnotations;
using todo_backend.Enums;

namespace todo_backend.DTOs;

public class CreateTodoDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; } = null!;

    [EnumDataType(typeof(TodoStatus))]
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
}
