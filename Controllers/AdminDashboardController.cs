using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDocDAL.Model;
using HalloDocBAL.Interfaces;

namespace HalloDoc.Controllers;

public class AdminDashboardController : Controller
{
    private readonly IAdminDashboardService _adminDashboardService;

    public AdminDashboardController(IAdminDashboardService adminDashboardService)
    {
        _adminDashboardService = adminDashboardService;
    }

    public IActionResult AdminDashboard()
    {
        var dash = _adminDashboardService.GetRequests();
        return View(dash);
    }

    
}
