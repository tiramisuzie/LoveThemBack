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

namespace LoveThemBackWebApp.Controllers
{
    public class PetController : Controller
    {

        private List<Pet> PetCollections { get; set; }
        public async Task<IActionResult> Index(string searchString)
        {
            dynamic Models = new ExpandoObject();
            PetCollections = await GetPetListJSON(searchString);
            Models.Pets = PetCollections;
            Models.Search = searchString;
            return View(Models);
        }

        public async Task<IActionResult> Details(int? id, string search)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<PetPost> PetList = await GetPetFromCustomAPI();
            PetPost GetPet = PetList.Where(pet => pet.PetID == id).FirstOrDefault();
            dynamic Models = new ExpandoObject();
            Models.Search = search;

            if (GetPet != null)
            {
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

        public async Task<IActionResult> CreateReview()
        {
            return View();
        }

        public async Task<List<Pet>> GetPetListJSON(string location)
        {
            if (location != null)
            {
                string url = "http://api.petfinder.com/pet.find?key=26d124a65947581b27aa9500628f49ef&location=" + location + "&format=json";
                using (var httpClient = new HttpClient())
                {
                    var json = await httpClient.GetStringAsync(url);
                    PetJSON retrieveJSON = JsonConvert.DeserializeObject<PetJSON>(json);

                    // Now parse with JSON.Net
                    return retrieveJSON.petfinder.pets.pet.ToList();
                }
            }
            return new List<Pet>();
        }

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

        public async Task<IActionResult> PetAPIPost(int? id, string search)
        {
            PetCollections = await GetPetListJSON(search);
            Pet SelectedPet = PetCollections.Where(x => x.id.data == id.ToString()).FirstOrDefault();
            string photos = null;
            if (SelectedPet != null)
            {
                foreach (var imageString in SelectedPet.media.photos.photo)
                {
                    photos = photos + "," + imageString.data;
                }
                PetPost AddPet = new PetPost()
                {
                    PetID = (int)id,
                    Animal = SelectedPet.animal.data,
                    Breed = SelectedPet.breeds.breed.ToString(),
                    Mix = SelectedPet.mix.data,
                    Name = SelectedPet.name.data,
                    Age = SelectedPet.age.data,
                    Sex = SelectedPet.sex.data,
                    Size = SelectedPet.size.data,
                    Description = SelectedPet.description.data,
                    ShelterID = SelectedPet.shelterId.data,
                    ShelterName = "",
                    Photos = photos,
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
