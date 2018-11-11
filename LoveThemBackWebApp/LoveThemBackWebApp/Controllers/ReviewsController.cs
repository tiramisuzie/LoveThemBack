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
    /// <summary>
    /// main review page
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    public IActionResult Index(Reviews review)
    {
      var userJSON = HttpContext.Session.GetString("profile");
      var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);
      dynamic model = new ExpandoObject();
      model.Review = review;
      model.User = userProfile;
      return View(model);
    }
    /// <summary>
    /// creates or edits review based on whether user already has review for pet
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([Bind("UserID, PetID, Impression, Affectionate, Friendly, HighEnergy, Healthy, Intelligent, Cheery, Playful")] Reviews review)
    {
      string url = "https://lovethembackapi2.azurewebsites.net/api/Reviews";
      using (HttpClient httpClient = new HttpClient())
      {
        var json = await httpClient.GetStringAsync(url);
        var retrieveJSON = JsonConvert.DeserializeObject<List<PetReview>>(json);

        var getReview = retrieveJSON.Where(x => x.UserID == review.UserID && x.PetID == review.PetID).FirstOrDefault();

        if (getReview != null)
        {
          var editReview = await _context.PutReview(review);
          return RedirectToAction("Details", "Pet", new { id = review.PetID });
        }
        else
        {
          var newReview = await _context.PostReview(review);
          return RedirectToAction("Details", "Pet", new { id = review.PetID });
        }
      }
    }
  }
}
