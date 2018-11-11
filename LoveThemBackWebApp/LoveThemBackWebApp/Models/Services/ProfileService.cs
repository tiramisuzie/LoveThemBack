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
    /// <summary>
    ///retrieves user profile from database
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<Profile> GetProfile(string username)
    {
      return await _context.Profiles.FirstOrDefaultAsync(x => x.Username == username);
    }


    /// <summary>
    /// Saves userprofile to database
    /// </summary>
    /// <param name="profile">user profile</param>
    /// <returns></returns>
    public async Task CreateProfile(Profile profile)
    {
      _context.Profiles.Add(profile);
      await _context.SaveChangesAsync();
    }
  }
}
