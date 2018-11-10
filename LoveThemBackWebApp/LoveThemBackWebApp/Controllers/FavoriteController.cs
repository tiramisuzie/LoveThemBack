using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
    /// <summary>
    /// Get list of favorites
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
      dynamic Models = new ExpandoObject();
      var userJSON = HttpContext.Session.GetString("profile");
      if (userJSON == null)
      {
        return RedirectToAction("Index", "Login");
      }
      var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);
      var favorites = await _context.GetFavorites(userProfile.UserID);
      Models.Favorites = favorites;
      Models.User = userProfile;
      return View(Models);
    }

    /// <summary>
    ///     Post to update new favorite
    /// </summary>
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
    [HttpPost]
    public async Task<IActionResult> Delete(int PetID)
    {
      var userJSON = HttpContext.Session.GetString("profile");
      var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);

      var favorite = await _context.GetFavorite(userProfile.UserID, PetID);
      await _context.DeleteFavorite(userProfile.UserID, PetID);

      if (favorite == null)
      {
        return NotFound();
      }

      return RedirectToAction(nameof(Index));
    }
  }
}
