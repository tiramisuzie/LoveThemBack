using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Models.Interfaces
{
    public interface IReview
    {
        ActionResult<IEnumerable<Review>> GetAll();

        ActionResult<IEnumerable<Review>> GetById(int id);

        Task<ActionResult> AddReview(Review review);

        ActionResult UpdateReview(int userId, int petId, Review review);

        ActionResult DeleteReview(int userId, int petId);
    }
}
