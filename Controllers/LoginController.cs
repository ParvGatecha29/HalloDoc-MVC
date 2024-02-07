﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Models;
using HalloDoc.DataContext;
using Newtonsoft.Json;
using HalloDoc.DataModel;

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
    public IActionResult PatientCreate()
    {
        return View();
    }

    [HttpPost]
    public JsonResult PatientLogin([FromBody] Login model)
    {
        var user = _context.Aspnetusers.FirstOrDefault(u=>
            u.Username == model.Username &&
            u.Passwordhash == model.Password);
        if (user != null) {
            return Json(new { success = true, redirectUrl = Url.Action("PatientDashboard", "Home") });
        }
        else
        {
            return Json(new { success = false, message = "Invalid Username or Password" });
        }
    }

    public async Task<JsonResult> PatientSignUp([FromBody] Register model)
    {
        if(ModelState.IsValid)
        {
            if(model.password != model.confirmpassword)
            {
                return Json(new { success = false, message = "Passwords donot match" });
            }

            var user = new Aspnetuser
            {
                Id = Guid.NewGuid().ToString(),
                Username = model.username,
                Createddate = DateTime.Now,
                Passwordhash = model.password,
 
        };

            _context.Aspnetusers.Add(user);
            await _context.SaveChangesAsync();

            return Json(new { success = true, redirectUrl = Url.Action("PatientDashboard", "Home") });
        }

        return Json(new { success = false, message = "Invalid Input" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
