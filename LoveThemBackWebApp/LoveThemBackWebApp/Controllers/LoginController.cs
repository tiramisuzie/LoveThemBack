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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            Profile profile = await _context.GetProfile(username, password);
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
