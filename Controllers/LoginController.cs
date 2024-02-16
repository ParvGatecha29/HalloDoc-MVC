﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalloDocDAL.Data;
using Newtonsoft.Json;
using HalloDocDAL.Models;
using System.ComponentModel.DataAnnotations;
using HalloDocDAL.Model;
using HalloDocBAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HalloDoc.Controllers;

public class LoginController : Controller
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
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
    public async Task<JsonResult> PatientLogin([FromBody] Login model)
    {
        var result = await _userService.Login(model);
        if (result) {
            Debug.WriteLine(model.Email);
            HttpContext.Session.SetString("email", model.Email);
            
            return Json(new { success = true, redirectUrl = Url.Action("PatientDashboard", "PatientDashboard") });
        }
        else
        {
            return Json(new { success = false, message = "Invalid Username or Password" });
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("PatientLogin","Login");
    }

    public async Task<JsonResult> PatientSignUp([FromBody] Register model)
    {
        if(ModelState.IsValid)
        {
            if(model.password != model.confirmpassword)
            {
                return Json(new { success = false, message = "Passwords donot match" });
            }

            var result = await _userService.SignUp(model);
            if (!result) { 
                return Json(new { success = false, message = "User already registered" });
            }

            return Json(new { success = true, redirectUrl = Url.Action("PatientDashboard", "PatientDashboard") });
        }

        return Json(new { success = false, message = "Invalid Input" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
