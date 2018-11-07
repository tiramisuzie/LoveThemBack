using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models.Interfaces
{
    public interface IProfiles
    {
        //Read
        Task<Profile> GetProfile(string userName, string password);
    }
}
