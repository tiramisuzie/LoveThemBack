using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoveThemBackWebApp.Controllers
{
    public class PetController: Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            return View();

        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
