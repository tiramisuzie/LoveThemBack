using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoveThemBackAPI.Models;
using LoveThemBackAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LoveThemBackAPI.Controllers
{
  [Route("api/Pets")]
  [ApiController]
  public class PetsController : ControllerBase
  {

    private readonly LoveThemBackAPIDbContext _context;
    /// <summary>
    /// this returns a sample pet if json string is initially null
    /// </summary>
    /// <param name="context"></param>
    public PetsController(LoveThemBackAPIDbContext context)
    {
      _context = context;
    }
    /// <summary>
    /// get list of all pet objects in json return
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<Pet>> GetAll()
    {
      var pet = _context.Pets.ToList();
      foreach (var item in pet)
      {
        var Reviews = _context.Reviews.Where(reviews => reviews.PetID == item.PetID).ToList();
      }
      return pet;
    }
    /// <summary>
    /// get specific PET data
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetPet")]
    public ActionResult<Pet> GetById(int id)
    {
      var item = _context.Pets.Find(id);
      var Reviews = _context.Reviews.Where(reviews => reviews.PetID == item.PetID).ToList();
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }
    /// <summary>
    /// adds pet object to json Pet api return
    /// </summary>
    /// <param name="Pet">pet object</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create(Pet Pet)
    {
      _context.Pets.Add(Pet);
      _context.SaveChanges();

      return CreatedAtRoute("GetPet", new { id = Pet.PetID }, Pet);
    }
    /// <summary>
    /// updates pet json content in api return
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Pet"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pet Pet)
    {
      var petReceived = _context.Pets.Find(id);
      if (petReceived == null)
      {
        return NotFound();
      }

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
      return CreatedAtRoute("GetPet", new { id = Pet.PetID }, Pet);
    }
    /// <summary>
    /// deletes pet content in api return body if right password is given
    /// </summary>
    /// <param name="id">pet id to be deleted</param>
    /// <param name="pass">the password</param>
    /// <returns></returns>
    [HttpDelete("{id}/{pass}")]
    public IActionResult Delete(int id, int pass)
    {
      if (pass == 8675309)
      {
        var petReceived = _context.Pets.Find(id);
        if (petReceived == null)
        {
          return NotFound();
        }

        _context.Pets.Remove(petReceived);
        _context.SaveChanges();
      }
      return NoContent();
    }
  }
}