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
    public PetsController(LoveThemBackAPIDbContext context)
    {
      _context = context;

      if (_context.Pets.Count() == 0)
      {
        Pet SamplePet = new Pet();
        SamplePet.PetID = 1;
        SamplePet.Name = "Sample Pet";
        SamplePet.Sex = Sex.Male;
        SamplePet.Description = "This is a sample Pet.";
        _context.Pets.Add(SamplePet);
        _context.SaveChanges();
      }
    }
    [HttpGet]
    public ActionResult<List<Pet>> GetAll()
    {
      return _context.Pets.ToList();
    }

    [HttpGet("{id}", Name = "GetPet")]
    public ActionResult<Pet> GetById(int id)
    {
      var item = _context.Pets.Find(id);
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }

    [HttpPost]
    public IActionResult Create(Pet Pet)
    {
      _context.Pets.Add(Pet);
      _context.SaveChanges();

      return CreatedAtRoute("GetTodo", new { id = Pet.PetID }, Pet);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pet Pet)
    {
      var petReceived = _context.Pets.Find(id);
      if (petReceived == null)
      {
        return NotFound();
      }

      petReceived.PetID = Pet.PetID;
      petReceived.Name = Pet.Name;
      petReceived.Age = Pet.Age;
      petReceived.Sex = Pet.Sex;
      petReceived.Description = Pet.Description;

      _context.Pets.Update(petReceived);
      _context.SaveChanges();
      return NoContent();
    }

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