using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Favorite>> GetFavorites(int userId)
        {
            return await _context.Favorites.ToListAsync(x => x.UserId == userId);
        }

        public async Task UpdateFavorite(Favorite hotel)
        {
            _context.Favorites.Update(hotel);
            await _context.SaveChangesAsync();
        }
    }
}
