using LoveThemBackWebApp.Models;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Routing;

namespace LoveThemBackWebApp.Controllers
{
  public class LoginController : Controller
  {
    private readonly IProfiles _context;

    public LoginController(IProfiles context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      var userJSON = HttpContext.Session.GetString("profile");
      if (userJSON != null)
      {
        return RedirectToAction("Index","Pet");
      }
      return View();
    }

    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index", "Login");
    }

    [HttpPost]
    public async Task<IActionResult> Index(string username)
    {
      Profile profile = await _context.GetProfile(username);
      HttpContext.Session.SetString("profile", JsonConvert.SerializeObject(profile));


      //Session["profile"] = profile;
      if (profile != null)
      {
        return RedirectToAction("Index", "Pet");
      }
      else return View();
    }
  }
}
