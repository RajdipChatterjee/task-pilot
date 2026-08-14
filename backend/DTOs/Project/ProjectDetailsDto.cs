namespace TaskPilot.Api.DTOs.Project;

public class ProjectDetailsDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Tasks { get; set; }
    public DateTime CreatedAt { get; set; }
}