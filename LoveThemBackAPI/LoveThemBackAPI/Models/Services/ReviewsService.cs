using LoveThemBackAPI.Data;
using LoveThemBackAPI.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Models.Services
{
  public class ReviewsService : IReview
  {
    private LoveThemBackAPIDbContext _context;

    public ReviewsService(LoveThemBackAPIDbContext context)
    {
      _context = context;
    }
    /// <summary>
    /// returns all reviews ever posted
    /// </summary>
    /// <returns></returns>
    public ActionResult<IEnumerable<Review>> GetAll()
    {
      return _context.Reviews.ToList();
    }
    /// <summary>
    /// retreives lists of all reviews from one pet
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Review>> GetById(int id)
    {
      var reviews = _context.Reviews.Where(x => x.PetID == id).ToList();

      return reviews;
    }
    /// <summary>
    /// allows api posting of a review
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    public async Task<ActionResult> AddReview(Review review)
    {
      await _context.Reviews.AddAsync(review);
      await _context.SaveChangesAsync();

      return null;
    }
    /// <summary>
    /// updates review to database
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="petId"></param>
    /// <param name="review"></param>
    /// <returns></returns>
    public ActionResult UpdateReview(int userId, int petId, Review review)
    {
      var reviewReceived = _context.Reviews.Find(petId, userId);

      reviewReceived.Impression = review.Impression;
      reviewReceived.Affectionate = review.Affectionate;
      reviewReceived.Friendly = review.Friendly;
      reviewReceived.HighEnergy = review.HighEnergy;
      reviewReceived.Healthy = review.Healthy;
      reviewReceived.Intelligent = review.Intelligent;
      reviewReceived.Cheery = review.Cheery;
      reviewReceived.Playful = review.Playful;
      reviewReceived.Drool = review.Drool;

      _context.Reviews.Update(reviewReceived);
      _context.SaveChanges();
      return null;
    }
    /// <summary>
    /// deletes reviews
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="petId"></param>
    /// <returns></returns>
    public ActionResult DeleteReview(int userId, int petId)
    {

      var reviewReceived = _context.Reviews.Find(petId, userId);

      _context.Reviews.Remove(reviewReceived);
      _context.SaveChanges();

      return null;
    }
  }
}
