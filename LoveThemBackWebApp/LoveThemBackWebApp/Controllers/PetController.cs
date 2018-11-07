using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoveThemBackWebApp.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace LoveThemBackWebApp.Controllers
{
  public class PetController : Controller
  {
    private static readonly HttpClient client = new HttpClient();

    public async Task<IActionResult> Index()
    {
      List<Pet> PetList = await GetJSON();
      return View(PetList);
    }

    public async Task<IActionResult> Details(int? id)
    {
      if (id == null) return NotFound();
      List<Pet> PetList = await GetJSON();
      List<PetReview> petReviews = await GetReviewJSON();
      var SelectedPet = PetList.Where(x => x.id.tspo == id.ToString()).ToList();
      var getReviews = petReviews.Where(item => item.PetID == id).ToList();
      foreach (var item in SelectedPet)
      {
        item.review = getReviews;
      }
      await PetAPIPost(id);
      return View(SelectedPet);

    }

    public async Task<IActionResult> Create()
    {
      return View();
    }

    public async Task<List<Pet>> GetJSON()
    {
      string url = "http://api.petfinder.com/pet.find?key=26d124a65947581b27aa9500628f49ef&location=98146&format=json";
      using (var httpClient = new HttpClient())
      {
        var json = await httpClient.GetStringAsync(url);
        PetJSON retrieveJSON = JsonConvert.DeserializeObject<PetJSON>(json);

        // Now parse with JSON.Net
        return retrieveJSON.petfinder.pets.pet.ToList();
      }
    }

    public async Task<List<PetReview>> GetReviewJSON()
    {
      string url = "https://lovethembackapi2.azurewebsites.net/api/Reviews";
      using (var httpClient = new HttpClient())
      {
        var json = await httpClient.GetStringAsync(url);
        var retrieveJSON = JsonConvert.DeserializeObject<List<PetReview>>(json);

        // Now parse with JSON.Net
        return retrieveJSON;
      }
    }

    public async Task<IActionResult> PetAPIPost(int? id)
    {
      List<Pet> PetList = await GetJSON();
      var SelectedPet = PetList.Where(x => x.id.tspo == id.ToString()).ToList();
      PetPost testPet = new PetPost()
      {
        PetID = (int)id,
        Name = SelectedPet[0].name.tsju
      };

      string output = await Task.Run(() => JsonConvert.SerializeObject(testPet));
      var httpContent = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
      using (var httpClient = new HttpClient())
      {
        // Error here
        var httpResponse = await httpClient.PostAsync("https://lovethembackapi2.azurewebsites.net/api/Pets", httpContent);
        if (httpResponse.Content != null)
        {
          // Error Here
          var responseContent = await httpResponse.Content.ReadAsStringAsync();
        }
      }
      return NoContent();
    }
  }
}
