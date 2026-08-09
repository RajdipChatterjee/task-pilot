using System.ComponentModel.DataAnnotations;
using TaskPilot.Api.Enums;

namespace TaskPilot.Api.DTOs.Todo;

public class UpdateTodoDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    [EnumDataType(typeof(TodoStatus))]
    public TodoStatus Status { get; set; }
}