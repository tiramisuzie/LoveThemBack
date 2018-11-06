using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("UserID,PetID,Impression,Affection,Friendly,Energy,Healthy,Intelligent,Cheery,Playful")] Reviews reviews)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(reviews);
                //await _context.SaveChangesAsync();
                string output = JsonConvert.SerializeObject(reviews);
                return RedirectToAction(nameof(Index));
            }
            return View(reviews);
        }
    }
}
