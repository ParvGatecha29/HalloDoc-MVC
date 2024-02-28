using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDocDAL.Model;
using HalloDocBAL.Interfaces;

namespace HalloDoc.Controllers;

public class AdminDashboardController : Controller
{
    private readonly IAdminDashboardService _adminDashboardService;
    int[] newcase = { 1 };
    int[] pendingcase = { 2 };
    int[] activecase = { 4,5 };
    int[] concludecase = { 6 };
    int[] toclosecase = { 3,7,8 };
    int[] unpaidcase = { 9 };
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
        var dash = _adminDashboardService.GetRequestsByStatus(newcase);
        return PartialView("_CaseTable",dash);
    }
    public IActionResult PendingCasePartial()
    {
        var dash = _adminDashboardService.GetRequestsByStatus(pendingcase);
        return PartialView("_CaseTable", dash);
    }
    public IActionResult ActiveCasePartial()
    {
        var dash = _adminDashboardService.GetRequestsByStatus(activecase);
        return PartialView("_CaseTable", dash);
    }
    public IActionResult ConcludeCasePartial()
    {
        var dash = _adminDashboardService.GetRequestsByStatus(concludecase);
        return PartialView("_CaseTable", dash);
    }
    public IActionResult ToCloseCasePartial()
    {
        var dash = _adminDashboardService.GetRequestsByStatus(toclosecase);
        return PartialView("_CaseTable", dash);
    }
    public IActionResult UnpaidCasePartial()
    {
        var dash = _adminDashboardService.GetRequestsByStatus(unpaidcase);
        return PartialView("_CaseTable", dash);
    }
    public IActionResult ViewCase(int requestid)
    {
        var dash = _adminDashboardService.GetRequestById(requestid);
        return View("ViewCase", dash);
    }
    public IActionResult ViewNotes(int requestid)
    {
        var dash = _adminDashboardService.GetNotes(requestid);
        return View("ViewNotes", dash);
    }
    public JsonResult UpdateNotes(int requestid, string notes)
    {
        var dash = _adminDashboardService.UpdateNotes(requestid, notes);
        return Json(new { success = true });
    }

    public JsonResult CancelCase(int Requestid, string reason, string info)
    {
        var dash = new AdminDashboardData
        {
            requestId = Requestid,
            reason = reason,
            notes = info
        };
        _adminDashboardService.CancelRequest(dash);
        return Json(new { success = true });
    }
}
