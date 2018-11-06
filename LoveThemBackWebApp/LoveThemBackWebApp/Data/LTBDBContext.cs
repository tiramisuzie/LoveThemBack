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

            modelBuilder.Entity<Favorite>().HasData(
                new Favorite
                {
                    UserID = 1,
                    PetID = 1,
                    Notes = "notes 1"
                },

                 new Favorite
                {
                    UserID = 2,
                    PetID = 2,
                    Notes = "notes 2",
                });
}
        }


        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

      
    }
}
