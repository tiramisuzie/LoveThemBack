using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveThemBackAPI.Data;
using LoveThemBackAPI.Models;
using LoveThemBackAPI.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoveThemBackAPI.Controllers
{
  [Route("api/Reviews")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private IReview _context;

    public ReviewsController(IReview context)
    {
      _context = context;
    }

    // GET: api/Reviews
    [HttpGet]
    public ActionResult<IEnumerable<Review>> Get()
    {
      return _context.GetAll();
    }

    // GET: api/Reviews/Pet=id
    [HttpGet("{id}", Name = "GetReview")]
    public ActionResult<IEnumerable<Review>> Get(int id)
    {
      var reviews = _context.GetById(id);

      return Ok(reviews);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Review review)
    {
      await _context.AddReview(review);

      return CreatedAtRoute("GetReview", new { id = review.PetID }, review);
    }
    /// <summary>
    /// updates reviews
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Pet"></param>
    /// <returns></returns>
    [HttpPut("{userId}/{petId}")]
    public IActionResult Update(int userId, int petId, Review review)
    {
      var reviewReceived = _context.UpdateReview(userId, petId, review);
      if (reviewReceived == null)
      {
        return NotFound();
      }

      return CreatedAtRoute("GetReview", new { id = review.PetID }, review);
    }

    [HttpDelete("{userId}/{petId}")]
    public ActionResult Delete(int userId, int petId)
    {
      _context.DeleteReview(userId, petId);
      return NoContent();
    }
  }
}
