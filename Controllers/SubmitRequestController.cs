using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using HalloDocDAL.Models;
using HalloDocDAL.Model;
using Microsoft.EntityFrameworkCore;
using HalloDocDAL.Data;
using Microsoft.AspNetCore.Identity;

namespace HalloDoc.Controllers;

public class SubmitRequestController : Controller
{
    private readonly ApplicationDbContext _context;

    public SubmitRequestController(ApplicationDbContext context)
    {
        _context = context;
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
    public JsonResult CheckEmailExists(string email)
    {
        var emailExists = _context.Aspnetusers.Any(x => x.Email == email);
        return Json(!emailExists);
    }

    [HttpPost]
    public async Task<JsonResult> PatRequest([FromBody] Req model)
    {
        if (ModelState.IsValid)
        {
            var aspuser = new Aspnetuser();
            if (!(_context.Aspnetusers.Any(x => x.Email == model.email)))
            {
                aspuser.Email = model.email;
                aspuser.Id = Guid.NewGuid().ToString();
                aspuser.Username = model.email;
                aspuser.Createddate = DateTime.Now;
                aspuser.Passwordhash = model.password;

                _context.Aspnetusers.Add(aspuser);
                await _context.SaveChangesAsync();
            }
            else
            {
                aspuser = _context.Aspnetusers.FirstOrDefault(x => x.Email == model.email);
            }
            var user = new User
            {
                Aspnetuserid = aspuser.Id,
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
