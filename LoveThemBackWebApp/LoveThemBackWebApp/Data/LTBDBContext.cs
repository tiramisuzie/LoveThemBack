using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveThemBackWebApp.Models;

namespace LoveThemBackWebApp.Data
{
    public class LTBDBContext : DbContext
    {
        public LTBDBContext(DbContextOptions<LTBDBContext> options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>().HasKey(
                fv => new { fv.UserID, fv.PetID }
                );
            modelBuilder.Entity<Profile>().HasData(
               new Profile
               {
                   UserID = 1,
                   Username = "username1",
                   LocationZip = 98144
               },

                new Profile
                {
                    UserID = 2,
                    Username = "username2",
                    LocationZip = 98144
                });
            modelBuilder.Entity<Favorite>().HasData(
                new Favorite
                {
                    UserID = 1,
                    PetID = 1,
                    Notes = "notes 1"
                },

                 new Favorite
                {
                    UserID = 1,
                    PetID = 2,
                    Notes = "notes 2",
                });

            
        }


        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
      
    }
}
