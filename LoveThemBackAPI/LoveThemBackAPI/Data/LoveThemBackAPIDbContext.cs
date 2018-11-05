using LoveThemBackAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Data
{
    public class LoveThemBackAPIDbContext : DbContext
    {

        public LoveThemBackAPIDbContext(DbContextOptions<LoveThemBackAPIDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasKey(
                r => new { r.PetID, r.UserID }
            );
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}
