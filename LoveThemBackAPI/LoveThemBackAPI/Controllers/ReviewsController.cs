﻿using System;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Get", new { id = review.PetID });
        }

        // PUT api/Reviews/UserID/PetID
        //[HttpPut("{UserID}/{PetID}")]
        //public async Task<IActionResult> Put(int UserID, int PetID, [FromBody] Review review)
        //{
        //    var result = _context.Reviews.FirstOrDefault(x => x.UserID == UserID && x.PetID == PetID);

        //    if (result != null)
        //    {
        //        _context.Reviews.Update(review);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        await Post(review);
        //    }

        //    return RedirectToAction("Get", new { id = PetID });
        //}
    }
}