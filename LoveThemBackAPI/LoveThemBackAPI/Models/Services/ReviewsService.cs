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

        public ActionResult<IEnumerable<Review>> GetAll()
        {
            return _context.Reviews.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Review>> GetById(int id)
        {
            var reviews = _context.Reviews.Where(x => x.PetID == id).ToList();

            return reviews;
        }

        public async Task<ActionResult> AddReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return null;
        }

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

        public ActionResult DeleteReview(int userId, int petId)
        {

            var reviewReceived = _context.Reviews.Find(petId, userId);

            _context.Reviews.Remove(reviewReceived);
            _context.SaveChanges();

            return null;
        }
    }
}
