using System.ComponentModel.DataAnnotations;
using TaskPilot.Api.Enums;

namespace TaskPilot.Api.DTOs.Todo;

public class CreateTodoDto
{
    [Required]
    public string ProjectId { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    [EnumDataType(typeof(TodoStatus))]
    public TodoStatus Status { get; set; } = TodoStatus.Pending;

    [Required]
    public DateTime TaskDate { get; set; }
}