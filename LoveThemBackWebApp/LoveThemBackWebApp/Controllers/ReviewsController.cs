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
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace LoveThemBackWebApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviews _context;

        public ReviewsController(IReviews context)
        {
            _context = context;
        }

        public IActionResult Index(Reviews review)
        {
            var userJSON = HttpContext.Session.GetString("profile");
            var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);
            dynamic model = new ExpandoObject();
            model.Review = review;
            model.User = userProfile;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserID, PetID, Impression, Affectionate, Friendly, Energy, Health, Intelligent, Cheery, Playful")] Reviews review)
        {
            var newReview = await _context.PostReview(review);
            return RedirectToAction("Index");
        }


        
    }
}
