using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoveThemBackAPI.Models;
using LoveThemBackAPI.Data;
using Microsoft.EntityFrameworkCore;
using LoveThemBackAPI.Models.Interfaces;

namespace LoveThemBackAPI.Controllers
{
  [Route("api/Pets")]
  [ApiController]
  public class PetsController : ControllerBase
  {

    private readonly IPet _context;
    /// <summary>
    /// this returns a sample pet if json string is initially null
    /// </summary>
    /// <param name="context"></param>
    public PetsController(IPet context)
    {
      _context = context;
    }
    /// <summary>
    /// get list of all pet objects in json return
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<Pet>> Get()
    {
       var pet = _context.GetAll();

       return pet;
    }
    /// <summary>
    /// get specific PET data
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetPet")]
    public ActionResult<Pet> Get(int id)
    {
      var item = _context.GetById(id);
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
    public ActionResult Create(Pet Pet)
    {
      _context.AddPet(Pet);

      return CreatedAtRoute("GetPet", new { id = Pet.PetID }, Pet);
    }
    /// <summary>
    /// updates pet json content in api return
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Pet"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult<Pet> Update(int id, Pet Pet)
    {
      var petReceived = _context.UpdatePet(id, Pet);

      if (petReceived == null)
      {
        return NotFound();
      }
        
      return CreatedAtRoute("GetPet", new { id = Pet.PetID }, Pet);
    }
    /// <summary>
    /// deletes pet content in api return body if right password is given
    /// </summary>
    /// <param name="id">pet id to be deleted</param>
    /// <param name="pass">the password</param>
    /// <returns></returns>
    [HttpDelete("{id}/{pass}")]
    public ActionResult Delete(int id, int pass)
    {
      _context.DeletePet(id, pass);
      return NoContent();
    }
  }
}