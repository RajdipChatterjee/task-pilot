using System.ComponentModel.DataAnnotations;

namespace TaskPilot.Api.DTOs.Project;

public class UpdateProjectDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }
}