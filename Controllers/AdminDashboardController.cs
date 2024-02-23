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
    public IActionResult NewCasePartial()
    {
        var dash = _adminDashboardService.GetRequests();
        return PartialView("_NewCase",dash);
    }
    public IActionResult PendingCasePartial()
    {
        var dash = _adminDashboardService.GetRequests();
        return PartialView("_PendingCase", dash);
    }
    public IActionResult ActiveCasePartial()
    {
        var dash = _adminDashboardService.GetRequests();
        return PartialView("_ActiveCase", dash);
    }
    public IActionResult ConcludeCasePartial()
    {
        var dash = _adminDashboardService.GetRequests();
        return PartialView("_ConcludeCase", dash);
    }
    public IActionResult ToCloseCasePartial()
    {
        var dash = _adminDashboardService.GetRequests();
        return PartialView("_ToCloseCase", dash);
    }
    public IActionResult UnpaidCasePartial()
    {
        var dash = _adminDashboardService.GetRequests();
        return PartialView("_UnpaidCase", dash);
    }
}
