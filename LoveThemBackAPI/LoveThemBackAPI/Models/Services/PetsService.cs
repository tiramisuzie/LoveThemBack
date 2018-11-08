using LoveThemBackAPI.Data;
using LoveThemBackAPI.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Models.Services
{
    public class PetsService : IPet
    {
        private readonly LoveThemBackAPIDbContext _context;

        public PetsService(LoveThemBackAPIDbContext context)
        {
            _context = context;
        }

        public ActionResult<List<Pet>> GetAll()
        {
            var pet = _context.Pets.ToList();
            foreach (var item in pet)
            {
                var Reviews = _context.Reviews.Where(reviews => reviews.PetID == item.PetID).ToList();
            }
            return pet;
        }

        public ActionResult<Pet> GetById(int id)
        {
            var item = _context.Pets.Find(id);
            var Reviews = _context.Reviews.Where(reviews => reviews.PetID == item.PetID).ToList();
            return item;
        }

        public ActionResult AddPet(Pet Pet)
        {
            _context.Pets.Add(Pet);
            _context.SaveChangesAsync();
            return null;
        }

        public ActionResult<Pet> UpdatePet(int id, Pet Pet)
        {
            var petReceived = _context.Pets.Find(id);

            petReceived.PetID = Pet.PetID;
            petReceived.Animal = Pet.Animal;
            petReceived.Breed = Pet.Breed;
            petReceived.Mix = Pet.Mix;
            petReceived.Name = Pet.Name;
            petReceived.Age = Pet.Age;
            petReceived.Sex = Pet.Sex;
            petReceived.Size = Pet.Size;
            petReceived.Description = Pet.Description;
            petReceived.ShelterID = Pet.ShelterID;
            petReceived.ShelterName = Pet.ShelterName;
            petReceived.Photos = Pet.Photos;
            petReceived.Address = Pet.Address;
            petReceived.City = Pet.City;
            petReceived.Zip = Pet.Zip;
            petReceived.State = Pet.State;
            petReceived.Phone = Pet.Phone;
            petReceived.Email = Pet.Email;

            _context.Pets.Update(petReceived);
            _context.SaveChanges();
            return petReceived;
        }

        public ActionResult DeletePet(int id, int pass)
        {
            if (pass == 8675309)
            {
                var petReceived = _context.Pets.Find(id);

                _context.Pets.Remove(petReceived);
                _context.SaveChanges();
            }
            return null;
        }
    }
}
