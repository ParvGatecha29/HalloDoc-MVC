using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Models;
using HalloDoc.DataContext;

namespace HalloDoc.Controllers;

public class LoginController : Controller
{
    private readonly ApplicationDbContext _context;

    public LoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult PatientLogin()
    {
        return View();
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public ActionResult PatientLogin(Login model)
    {
        var user = _context.Aspnetusers.FirstOrDefault(u=>
            u.Username == model.Username &&
            u.Passwordhash == model.Password);
        if (user != null) {
            return RedirectToAction("PatientDashboard", "Home");
        }
        else
        {
            return RedirectToAction("PatientLogin", "Login");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
