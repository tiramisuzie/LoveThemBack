using LoveThemBackWebApp.Models;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var value = HttpContext.Session.GetString("profile");
            Profile result;
            if (value != null)
            {
                result = JsonConvert.DeserializeObject<Profile>(value);
            }

            return View();
        }
        
    }
}
