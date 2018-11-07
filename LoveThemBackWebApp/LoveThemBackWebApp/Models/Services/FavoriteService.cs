using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models.Services
{
    public class FavoriteService : IFavorites
    {
        private LTBDBContext _context;

        public FavoriteService(LTBDBContext context)
        {
            _context = context;
        }

        public async Task CreateFavorite(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFavorite(int userId, int petId)
        {
            Favorite favorite = await GetFavorite(userId, petId);
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<Favorite> GetFavorite(int userId, int petId)
        {
            return await _context.Favorites.FirstOrDefaultAsync(x => x.UserID == userId && x.PetID == petId);
        }

        public async Task<List<PetPost>> GetFavorites(int userId)
        {
            var pets = await GetJSON();
            List<Favorite> favorites = _context.Favorites.Where(x => x.UserID == userId).ToList();

            List<PetPost> myFavPets = new List<PetPost>();

            //foreach (var pet in favorites)
            //{
            //    foreach (var item in pets)
            //    //{
            //    //    if (item.id. == pet.PetID.ToString())
            //    //    {
            //    //        myFavPets.Add(item);
            //    //    }
            //    //}
            //}
            return pets;
        }

        public async Task UpdateFavorite(Favorite favorite)
        {
            _context.Favorites.Update(favorite);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<Pet>> GetJSON()
        public async Task<List<PetPost>> GetJSON()
        {
            string url = "https://lovethembackapi2.azurewebsites.net/api/Pets";
            using (var httpClient = new HttpClient())
            {
                
                    var json = await httpClient.GetStringAsync(url);
                    var retrieveJSON = JsonConvert.DeserializeObject<List<PetPost>>(json);
                
                // Now parse with JSON.Net
                return retrieveJSON;
            }
        }
    }
}
