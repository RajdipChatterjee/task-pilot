using System.ComponentModel.DataAnnotations;

namespace todo_backend.DTOs;

public class CreateTodoDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; } = null!;
}
