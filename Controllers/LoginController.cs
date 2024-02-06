using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Models;

namespace HalloDoc.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public LoginController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult PatientLogin()
    {
        return View();
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
