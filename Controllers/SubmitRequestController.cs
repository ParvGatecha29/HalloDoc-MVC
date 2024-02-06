using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Models;

namespace HalloDoc.Controllers;

public class SubmitRequestController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public SubmitRequestController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult SubmitRequest()
    {
        return View();
    }

    public IActionResult PatientRequest()
    {
        return View();
    }

    public IActionResult ConciergeRequest()
    {
        return View();
    }

    public IActionResult BusinessRequest()
    {
        return View();
    }

    public IActionResult FamilyRequest()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
