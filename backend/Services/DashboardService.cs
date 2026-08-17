using TaskPilot.Api.DTOs.Dashboard;
using TaskPilot.Api.Interfaces;

namespace TaskPilot.Api.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;
    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }
    public async Task<DashboardDto> GetDashboardDataAsync(string userId)
    {
        try
        {
            return await _dashboardRepository.GetDashboardDataAsync(userId);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving dashboard data: {ex.Message}", ex);
        }
    }
}
