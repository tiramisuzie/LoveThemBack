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
using System.Dynamic;
using LoveThemBackWebApp.Data;
using Microsoft.AspNetCore.Http;

namespace LoveThemBackWebApp.Controllers
{
  public class PetController : Controller
  {
    private readonly LTBDBContext _context;


    public PetController(LTBDBContext context)
    {
      _context = context;
    }

    private List<Pet> PetCollections { get; set; }
    /// <summary>
    /// main pet search page to render list of all pets based on zip code
    /// </summary>
    /// <param name="searchString"></param>
    /// <returns></returns>
    public async Task<IActionResult> Index(string searchString)
    {
      var userJSON = HttpContext.Session.GetString("profile");
      if (userJSON == null)
      {
        return RedirectToAction("Index", "Login");
      }
      var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);
      dynamic Models = new ExpandoObject();
      if (searchString == null)
      {
        PetCollections = await GetPetListJSON(userProfile.LocationZip.ToString());
      }
      else
      {
        PetCollections = await GetPetListJSON(searchString);
      }
      Models.Pets = PetCollections;
      Models.Search = string.IsNullOrEmpty(searchString) ? userProfile.LocationZip.ToString() : searchString;
      Models.User = userProfile;
      return View(Models);
    }
    /// <summary>
    /// takes you to main detail page, adds pet to api database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<IActionResult> Details(int? id, string search)
    {
      var userJSON = HttpContext.Session.GetString("profile");
      if (userJSON == null)
      {
        return RedirectToAction("Index", "Login");
      }
      var userProfile = JsonConvert.DeserializeObject<Profile>(userJSON);

      if (id == null)
      {
        return NotFound();
      }
      List<PetPost> PetList = await GetPetFromCustomAPI();
      PetPost GetPet = PetList.Where(pet => pet.PetID == id).FirstOrDefault();
      List<Profile> Users = new List<Profile>();
      dynamic Models = new ExpandoObject();
      Models.Search = search;
      Models.User = userProfile;

      if (GetPet != null)
      {
        if (GetPet.Review != null)
        {
          foreach (var item in GetPet.Review)
          {
            Profile User = await _context.Profiles.FirstOrDefaultAsync(x => x.UserID == item.UserID);
            Users.Add(User);
          }
        }
        Models.ReviewUser = Users;
        Models.GetPet = GetPet;
        return View(Models);
      }
      else
      {
        await PetAPIPost(id, search);
        PetList = await GetPetFromCustomAPI();
        GetPet = PetList.Where(pet => pet.PetID == id).FirstOrDefault();
        Models.GetPet = GetPet;
        return View(Models);
      }
    }
    /// <summary>
    /// adds pet to favorite table database
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="petID"></param>
    /// <returns></returns>
    public async Task<IActionResult> AddFavorites(int userID, int petID)
    {
      Favorite favorite = await _context.Favorites.FirstOrDefaultAsync(x => x.UserID == userID && x.PetID == petID);
      if (favorite == null)
      {
        Favorite FavoritePet = new Favorite()
        {
          UserID = userID,
          PetID = petID,
        };
        _context.Favorites.Add(FavoritePet);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction("Details", "Pet", new { id = petID });
    }
    /// <summary>
    /// petfinder api call to render pet list
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public async Task<List<Pet>> GetPetListJSON(string location)
    {
      if (location != null)
      {
        string url = "http://api.petfinder.com/pet.find?key=26d124a65947581b27aa9500628f49ef&location=" + location + "&format=json";
        using (var httpClient = new HttpClient())
        {
          var jsonstatus = await httpClient.GetAsync(url);
          if (!jsonstatus.IsSuccessStatusCode)
          {
            return new List<Pet>();
          }
          var json = await httpClient.GetStringAsync(url);
          PetJSON retrieveJSON = JsonConvert.DeserializeObject<PetJSON>(json);
          // Now parse with JSON.Net
          if (retrieveJSON.petfinder.pets == null)
          {
            return new List<Pet>();
          }
          return retrieveJSON.petfinder.pets.pet.ToList();
        }
      }
      return new List<Pet>();
    }
    /// <summary>
    /// redirects to review page with petid of pet being viewed in details
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    public IActionResult Review([Bind("PetID")] Reviews review)
    {
      return RedirectToAction("Index", "Reviews", review);
    }
    /// <summary>
    /// third party API call for specific pet. Maybe of use later.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Pet> GetPetByIDJSON(int? id)
    {
      string url = $"http://api.petfinder.com/pet.get?key=26d124a65947581b27aa9500628f49ef&id=" + id + "&format=json";
      using (HttpClient httpClient = new HttpClient())
      {
        var json = await httpClient.GetStringAsync(url);
        var retrieveJSON = JsonConvert.DeserializeObject<PetJSON>(json);
        return retrieveJSON.petfinder.pet;
      }
    }
    /// <summary>
    /// custom api call for reviews
    /// </summary>
    /// <returns></returns>
    public async Task<List<PetReview>> GetReviewJSON()
    {
      string url = "https://lovethembackapi2.azurewebsites.net/api/Reviews";
      using (HttpClient httpClient = new HttpClient())
      {
        var json = await httpClient.GetStringAsync(url);
        var retrieveJSON = JsonConvert.DeserializeObject<List<PetReview>>(json);
        return retrieveJSON;
      }
    }
    /// <summary>
    /// custom api call for list of pets
    /// </summary>
    /// <returns></returns>
    public async Task<List<PetPost>> GetPetFromCustomAPI()
    {
      string url = $"https://lovethembackapi2.azurewebsites.net/api/Pets/";
      using (HttpClient httpClient = new HttpClient())
      {
        var json = await httpClient.GetStringAsync(url);
        var retrieveJSON = JsonConvert.DeserializeObject<List<PetPost>>(json);
        return retrieveJSON.ToList();
      }
    }
    /// <summary>
    /// This method posts to the Pet endpoint of the custom API using data from petfinder API
    /// when the user clicks on the pet to see more in details
    /// </summary>
    /// <param name="id">the petfinder pet id</param>
    /// <param name="search">the location parameter to get list of pets from Petfinder</param>
    /// <returns></returns>
    public async Task<IActionResult> PetAPIPost(int? id, string search)
    {
      PetCollections = await GetPetListJSON(search);
      Pet SelectedPet = PetCollections.Where(x => x.id.data == id.ToString()).FirstOrDefault();
      if (SelectedPet != null)
      {
        string[] image = new string[SelectedPet.media.photos.photo.Count()];
        string[] breed = new string[SelectedPet.breeds.breed.Count()];
        for (int i = 0; i < SelectedPet.media.photos.photo.Count(); i++)
        {
          image[i] = SelectedPet.media.photos.photo[i].data;
        }

        for (int i = 0; i < SelectedPet.breeds.breed.Count(); i++)
        {
          breed[i] = SelectedPet.breeds.breed[i].data;
        }

        string images = string.Join(",", image);
        string breeds = string.Join(",", breed);

        PetPost AddPet = new PetPost()
        {
          PetID = (int)id,
          Animal = SelectedPet.animal.data,
          Breed = breeds,
          Mix = SelectedPet.mix.data,
          Name = SelectedPet.name.data,
          Age = SelectedPet.age.data,
          Sex = SelectedPet.sex.data,
          Size = SelectedPet.size.data,
          Description = SelectedPet.description.data,
          ShelterID = SelectedPet.shelterId.data,
          ShelterName = "",
          Photos = images,
          Address = SelectedPet.contact.address1.data,
          City = SelectedPet.contact.city.data,
          Zip = SelectedPet.contact.zip.data,
          State = SelectedPet.contact.state.data,
          Phone = SelectedPet.contact.phone.data,
          Email = SelectedPet.contact.email.data,
        };

        string output = await Task.Run(() => JsonConvert.SerializeObject(AddPet));
        var httpContent = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
        using (HttpClient httpClient = new HttpClient())
        {
          var httpResponse = await httpClient.PostAsync("https://lovethembackapi2.azurewebsites.net/api/Pets", httpContent);
          if (httpResponse.Content != null)
          {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
          }
        }
      }
      return NoContent();
    }
  }
}