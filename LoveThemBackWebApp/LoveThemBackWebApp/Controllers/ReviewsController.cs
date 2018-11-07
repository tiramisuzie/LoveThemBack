using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly LTBDBContext _context;

        public ReviewsController(LTBDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
