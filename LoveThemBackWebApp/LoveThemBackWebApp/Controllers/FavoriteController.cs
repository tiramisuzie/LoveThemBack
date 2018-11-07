using LoveThemBackWebApp.Data;
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
            var userJSON = HttpContext.Session.GetString("profile");
            var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);
            var favorites = await _context.GetFavorites(userProfile.UserID);
            return View(favorites);
        }

        //Post to update new favorite
        [HttpPost]
        public async Task<IActionResult> Update(int PetID, string Notes)
        {
            var userJSON = HttpContext.Session.GetString("profile");
            var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);

            Favorite favorite = new Favorite()
            {
                UserID = userProfile.UserID,
                PetID = PetID,
                Notes = Notes
            };

            await _context.UpdateFavorite(favorite);
            return RedirectToAction(nameof(Index));
        }

        // GET: Favorites/Delete/5
        public async Task<IActionResult> Delete(int UserID, int PetID)
        {
            var favorite = await _context.GetFavorite(UserID, PetID);

            if (favorite == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Favorite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int UserID, int PetID)
        {
            await _context.DeleteFavorite(UserID, PetID);

            return RedirectToAction(nameof(Index));
        }
    }
}
