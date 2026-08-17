using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskPilot.Api.Common;
using TaskPilot.Api.DTOs.Dashboard;
using TaskPilot.Api.Interfaces;

namespace TaskPilot.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<DashboardDto>>> GetAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized(new ApiResponse<DashboardDto>(false, null, "User not found", null));

        try
        {
            var dashboardData = await _dashboardService.GetDashboardDataAsync(userId);
            return Ok(new ApiResponse<DashboardDto>(true, dashboardData, "Dashboard data retrieved successfully", null));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<DashboardDto>(false, null, "Failed to retrieve dashboard data", [ex.Message]));
        }
    }
}