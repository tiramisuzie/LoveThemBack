using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoveThemBackAPI.Models;
using LoveThemBackAPI.Data;

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
  }
}