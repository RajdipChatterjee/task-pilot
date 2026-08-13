namespace TaskPilot.Api.DTOs.Project;

public class ProjectDetailsDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Tasks { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
}