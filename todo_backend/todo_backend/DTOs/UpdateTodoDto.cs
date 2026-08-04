using System.ComponentModel.DataAnnotations;

namespace todo_backend.DTOs;

public class UpdateTodoDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
}