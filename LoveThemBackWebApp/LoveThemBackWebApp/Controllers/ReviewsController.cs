using LoveThemBackWebApp.Models;
using LoveThemBackWebApp.Models.Interfaces;
using LoveThemBackWebApp.Data;
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
        private readonly IReviews _context;

        public ReviewsController(IReviews context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserID, PetID, Impression, Affectionate, Friendly, Energy, Health, Intelligent, Cheery, Playful")] Reviews review)
        {
            var newReview = await _context.PostReview(review);
            return RedirectToAction();
        }


        
    }
}
