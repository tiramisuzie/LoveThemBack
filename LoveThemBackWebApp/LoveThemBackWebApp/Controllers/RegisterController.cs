using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveThemBackWebApp.Models;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoveThemBackWebApp.Controllers
{
  public class RegisterController : Controller
  {
    private readonly IProfiles _context;

    public RegisterController(IProfiles context)
    {
      _context = context;
    }
    /// <summary>
    /// main create account page
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
      return View();
    }
    /// <summary>
    /// creates user profile from information user inputs
    /// </summary>
    /// <param name="username"></param>
    /// <param name="locationZip"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Index(string username, int locationZip)
    {
      var existProfile = await _context.GetProfile(username);
      if (existProfile == null)
      {
        Profile profile = new Profile();
        profile.Username = username;
        profile.LocationZip = locationZip;
        await _context.CreateProfile(profile);
        HttpContext.Session.SetString("profile", JsonConvert.SerializeObject(profile));


        //Session["profile"] = profile;
        if (profile != null)
        {
          return RedirectToAction("Index","Pet");
        }
      }
      var error = new RegisterError();
      error.userExist = true;
      return View(error);

    }
  }
}