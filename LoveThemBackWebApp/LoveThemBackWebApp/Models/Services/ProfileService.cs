using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models.Services
{
    public class ProfileService : IProfiles
    {
        private LTBDBContext _context;

        public ProfileService(LTBDBContext context)
        {
            _context = context;
        }

        public async Task<Profile> GetProfile (string username, string password)
        {
            return await _context.Profiles.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}
