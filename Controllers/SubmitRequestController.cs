using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using HalloDocDAL.Models;
using HalloDocDAL.Model;
using Microsoft.EntityFrameworkCore;
using HalloDocDAL.Data;
using Microsoft.AspNetCore.Identity;
using HalloDocBAL.Interfaces;

namespace HalloDoc.Controllers;

public class SubmitRequestController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;
    private readonly IRequestService _requestService;


    public SubmitRequestController(ApplicationDbContext context,IUserService userService, IRequestService requestService)
    {
        _context = context;
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
        Console.WriteLine(emailExists != null);
        if (emailExists != null)
        {
            return Json(true);
        }
        return Json(false);
    }

    [HttpPost]
    public async Task<JsonResult> PatRequest([FromBody] Req model)
    {
        if (ModelState.IsValid)
        {
            var aspuser = new Aspnetuser();
            var user = new User();
            if (!(await CheckEmailExists(model.email) == Json(false)))
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

        return Json(new { success = false, message = "Invalid Input" });
    }

    [HttpPost]
    public async Task<JsonResult> ConRequest([FromBody] Req model)
    {
        if (ModelState.IsValid)
        {
            var concierge = new Concierge
            {
                Conciergename = model.cfirstName,
                Address = model.street + model.city,
                Street = model.street,
                City = model.city,
                State = model.state,
                Zipcode = model.zipcode,
                Createddate = DateTime.Now
            };
            _context.Concierges.Add(concierge);
            await _context.SaveChangesAsync();

            var user = new User
            {
                Firstname = model.firstName,
                Lastname = model.lastName,
                Email = model.email,
                Mobile = model.phone,
                Street = model.street,
                City = model.city,
                State = model.state,
                Zipcode = model.zipcode
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var request = new Request
            {
                Requesttypeid = model.typeid,
                Userid = user.Userid,
                Firstname = model.firstName,
                Lastname = model.lastName,
                Phonenumber = model.phone,
                Email = model.email,
                Createddate = DateTime.Now
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return Json(new { success = true, redirectUrl = Url.Action("SubmitRequest", "SubmitRequest") });
        }

        return Json(new { success = false, message = "Invalid Input" });
    }

    [HttpPost]
    public async Task<JsonResult> BusRequest([FromBody] Req model)
    {
        if (ModelState.IsValid)
        {
            var business = new Business
            {
                Name = model.cfirstName,
                Address1 = model.street + model.city,
                City = model.city,
                Zipcode = model.zipcode,
                Createddate = DateTime.Now,
                Phonenumber = model.cphone
            };
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();

            var user = new User
            {
                Firstname = model.firstName,
                Lastname = model.lastName,
                Email = model.email,
                Mobile = model.phone,
                Street = model.street,
                City = model.city,
                State = model.state,
                Zipcode = model.zipcode
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var request = new Request
            {
                Requesttypeid = model.typeid,
                Userid = user.Userid,
                Firstname = model.firstName,
                Lastname = model.lastName,
                Phonenumber = model.phone,
                Email = model.email,
                Createddate = DateTime.Now
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

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
