using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavorites _context;

        public FavoriteController(IFavorites context)
        {
            _context = context;
        }

        //Get list of favorites
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var test = await _context.GetFavorites(1);
            return View(test);
        }
    }
}
