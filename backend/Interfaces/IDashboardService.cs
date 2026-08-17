using Microsoft.AspNetCore.Mvc;
using TaskPilot.Api.Common;
using TaskPilot.Api.DTOs.Dashboard;

namespace TaskPilot.Api.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardDataAsync(string userId);
}
