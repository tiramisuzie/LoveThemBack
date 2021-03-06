﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models.Interfaces
{
    public interface IFavorites
    {
        //Create
        Task CreateFavorite(Favorite favorite);
        //Read
        Task<Favorite> GetFavorite(int UserID, int PetID);

        Task<List<PetPost>> GetFavorites(int userId);
        //Update
        Task UpdateFavorite(Favorite favorite);
        //Delete
        Task DeleteFavorite(int userId, int petId);
    }
}
