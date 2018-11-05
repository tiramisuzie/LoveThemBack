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
    [Route("api/[controller]")]
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

        // GET: api/Reviews/id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Review>> Get(int id)
        {
            var reviews = _context.Reviews.Where(x => x.PetID == id).ToList();

            return Ok(reviews);
        }
    }
}