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
  [Route("api/[controller]")]
  [ApiController]
  public class PetsController : ControllerBase
  {

    private readonly LoveThemBackAPIDbContext _context;
    public PetsController(LoveThemBackAPIDbContext context)
    {
      _context = context;

      if (_context.Reviews.Count() == 0)
      {
        Pet SamplePet = new Pet();
        SamplePet.PetID = 0;
        SamplePet.Name = "Sample Pet";
        SamplePet.Sex = Sex.Male;
        SamplePet.Description = "This is a sample Pet.";
        _context.Pets.Add(SamplePet);
      }
    }
  }
}