using TaskPilot.Api.DTOs.Dashboard;

namespace TaskPilot.Api.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardDataAsync(string userId);
}