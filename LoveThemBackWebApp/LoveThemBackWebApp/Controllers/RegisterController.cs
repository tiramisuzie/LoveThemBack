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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, int locationZip)
        {
            Profile profile = new Profile();
            profile.Username = username;
            profile.LocationZip = locationZip;
            await _context.CreateProfile(profile);
            HttpContext.Session.SetString("profile", JsonConvert.SerializeObject(profile));


            //Session["profile"] = profile;
            if (profile != null)
            {
                return RedirectToAction("Index", "Pet", new { id = username });
            }
            else return View();
        }
    }
}