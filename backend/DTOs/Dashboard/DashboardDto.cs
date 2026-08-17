using System.Runtime.InteropServices;

namespace TaskPilot.Api.DTOs.Dashboard;

public class DashboardDto
{
    public int TotalProjects { get; set; }
    public int TotalTasks { get; set; }
    public int TotalCompletedTasks { get; set; }
    public int TotalPendingTasks { get; set; }
}