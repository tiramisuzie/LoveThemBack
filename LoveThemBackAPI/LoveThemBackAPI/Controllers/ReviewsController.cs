using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveThemBackAPI.Data;
using LoveThemBackAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoveThemBackAPI.Controllers
{
  [Route("api/Reviews")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private LoveThemBackAPIDbContext _context;

    public ReviewsController(LoveThemBackAPIDbContext context)
    {
      _context = context;
    }

    // GET: api/Reviews
    public ActionResult<IEnumerable<Review>> Get()
    {
      return _context.Reviews.ToList();
    }

    // GET: api/Reviews/Pet=id
    [HttpGet("Pet={id}")]
    public ActionResult<IEnumerable<Review>> Get(int id)
    {
      var reviews = _context.Reviews.Where(x => x.PetID == id).ToList();

      return Ok(reviews);
    }

    [HttpPost]
    public IActionResult Create(Review review)
    {
      _context.Reviews.Add(review);
      _context.SaveChanges();

      return NoContent();
    }
    /// <summary>
    /// updates reviews
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Pet"></param>
    /// <returns></returns>
    [HttpPut("userid={userId}&petid={petId}")]
    public IActionResult Update(int userId, int petId, Review review)
    {
      var reviewReceived = _context.Reviews.Find(petId, userId);
      if (reviewReceived == null)
      {
        return NotFound();
      }

      reviewReceived.Impression = review.Impression;
      reviewReceived.Affectionate = review.Affectionate;
      reviewReceived.Friendly = review.Friendly;
      reviewReceived.HighEnergy = review.HighEnergy;
      reviewReceived.Healthy = review.Healthy;
      reviewReceived.Intelligent = review.Intelligent;
      reviewReceived.Cheery = review.Cheery;
      reviewReceived.Playful = review.Playful;

      _context.Reviews.Update(reviewReceived);
      _context.SaveChanges();
      return NoContent();
    }

    [HttpDelete("userid={id}&petid={pet}")]
    public IActionResult Delete(int id, int pet)
    {

        var reviewReceived = _context.Reviews.Find(id, pet);
        if (reviewReceived == null)
        {
          return NotFound();
        }

        _context.Reviews.Remove(reviewReceived);
        _context.SaveChanges();

      return NoContent();
    }
  }
}
