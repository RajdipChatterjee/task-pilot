using System.Runtime.InteropServices;
using TaskPilot.Api.DTOs.Project;

namespace TaskPilot.Api.DTOs.Dashboard;

public class DashboardDto
{
    public int TotalProjects { get; set; }
    public int TotalTasks { get; set; }
    public int TotalCompletedTasks { get; set; }
    public int TotalPendingTasks { get; set; }
    public List<ProjectDetailsDto> RecentProjects { get; set; }
}