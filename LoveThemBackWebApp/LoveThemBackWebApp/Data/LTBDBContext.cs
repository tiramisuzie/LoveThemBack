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
        }


        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
      
    }
}
