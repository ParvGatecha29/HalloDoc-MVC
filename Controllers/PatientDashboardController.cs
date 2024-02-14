using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDocDAL.Data;
using Newtonsoft.Json;
using HalloDocDAL.Models;
using System.ComponentModel.DataAnnotations;
using HalloDocDAL.Model;
using HalloDocBAL.Interfaces;
using HalloDocBAL.Services;

namespace HalloDoc.Controllers;

public class PatientDashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public PatientDashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }
    
    public IActionResult PatientDashboard()
    {
        var email = HttpContext.Session.GetString("email");
        ViewData["email"] = email;
        if (email == null)
        {
            return RedirectToAction("PatientLogin", "RegisterPatient");
        }
        return View(_dashboardService.PatientDashboard(email));
    }

    public IActionResult ViewDocument(int id)
    {
        return View(_dashboardService.ViewDocument(id));
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
