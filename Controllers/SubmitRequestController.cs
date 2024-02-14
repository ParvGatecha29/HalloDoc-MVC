using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using HalloDocDAL.Models;
using HalloDocDAL.Model;
using Microsoft.EntityFrameworkCore;
using HalloDocDAL.Data;
using Microsoft.AspNetCore.Identity;
using HalloDocBAL.Interfaces;
using HalloDoc.Views.Home;

namespace HalloDoc.Controllers;

public class SubmitRequestController : Controller
{
    private readonly IUserService _userService;
    private readonly IRequestService _requestService;


    public SubmitRequestController(IUserService userService, IRequestService requestService)
    {
        
        _userService = userService;
        _requestService = requestService;
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

    [HttpPost]
    public async Task<JsonResult> CheckEmailExists(string email)
    {
        var emailExists = await _userService.CheckUser(email);
        if (emailExists != null)
        {
            return Json(true);
        }
        return Json(false);
    }

    [HttpPost]
    public async Task<JsonResult> PatRequest(IFormCollection formcollection, Req model)
    {

        model.email = formcollection["patientEmail"];
        model.cfirstName = formcollection["cfname"];
        model.clastName = formcollection["clname"];
        model.cphone = formcollection["ctel"];
        model.cemail = formcollection["cemail"];
        model.propbus = formcollection["cprop"];
        model.symptoms = formcollection["symptoms"];
        model.firstName = formcollection["fname"];
        model.lastName = formcollection["lname"];
        model.password = formcollection["password"];
        model.dob = formcollection["dob"];
        model.phone = formcollection["patientTel"];
        model.street = formcollection["street"];
        model.city = formcollection["city"];
        model.state = formcollection["state"];
        model.zipcode = formcollection["zipcode"];
        model.room = formcollection["roomNum"];
        model.typeid = 1;
        model.document = formcollection.Files;
        Debug.WriteLine(model.document.Count);
            var aspuser = new Aspnetuser();
            var user = new User();
            if ((await CheckEmailExists(model.email) == Json(false)))
            {
                var reg = new Register
                {
                    Email = model.email,
                    password = model.password
                };
                var result = await _userService.SignUp(reg);
                aspuser = await _userService.CheckUser(model.email);

                user.Email = model.email;
                user.Firstname = model.firstName;
                user.Lastname = model.lastName;
                user.Aspnetuserid = aspuser.Id;
                await _userService.AddUser(user);
            }
            else
            {
                aspuser = await _userService.CheckUser(model.email);
            }

            await _requestService.PatientRequest(model);

            return Json(new { success = true, redirectUrl = Url.Action("SubmitRequest", "SubmitRequest") });
        
    }

    [HttpPost]
    public async Task<JsonResult> FamRequest(IFormCollection formcollection,Req model)
    {
        model.email = formcollection["patientEmail"];
        model.cfirstName = formcollection["cfname"];
        model.clastName = formcollection["clname"];
        model.cphone = formcollection["ctel"];
        model.cemail = formcollection["cemail"];
        model.propbus = formcollection["cprop"];
        model.symptoms = formcollection["symptoms"];
        model.firstName = formcollection["fname"];
        model.lastName = formcollection["lname"];
        model.password = formcollection["password"];
        model.dob = formcollection["dob"];
        model.phone = formcollection["patientTel"];
        model.street = formcollection["street"];
        model.city = formcollection["city"];
        model.state = formcollection["state"];
        model.zipcode = formcollection["zip"];
        model.room = formcollection["roomNum"];
        model.typeid = 2;
        
        

            await _requestService.PatientRequest(model);

            return Json(new { success = true, redirectUrl = Url.Action("SubmitRequest", "SubmitRequest") });
        

        
    }

    [HttpPost]
    public async Task<JsonResult> ConRequest([FromBody] Req model)
    {
        if (ModelState.IsValid)
        {
            await _requestService.ConciergeRequest(model);

            return Json(new { success = true, redirectUrl = Url.Action("SubmitRequest", "SubmitRequest") });
        }

        return Json(new { success = false, message = "Invalid Input" });
    }

    [HttpPost]
    public async Task<JsonResult> BusRequest([FromBody] Req model)
    {
        if (ModelState.IsValid)
        {
            await _requestService.BusinessRequest(model);

            return Json(new { success = true, redirectUrl = Url.Action("SubmitRequest", "SubmitRequest") });
        }

        return Json(new { success = false, message = "Invalid Input" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
