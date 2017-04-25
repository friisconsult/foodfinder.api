using System;
using FoodFinder.API.Authentication;
using FoodFinder.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ApplicationKeyAttributem))]
    public class ReviewsController : Controller
    {
        private readonly FoodFinderContext _context;

        public ReviewsController(FoodFinderContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Review([FromRoute] Guid id)
        {
            var comment = _context.Reviews.Find(id);
            if (comment != null)
                return Ok(comment);
            else
                return NotFound($"No review with id {id} was found in the dataase");
        }


        [HttpPost]
        [ServiceFilter(typeof(ApplicationKeyAttributem))]
        public IActionResult PostReview([FromBody] Review review)
        {
            review.Venue = _context.Venues.Find(review.VenueId);
            if (review.Venue == null)
                return BadRequest($"There is no venue with id:{review.VenueId}");

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Created(review.Id.ToString(), review);
        }
    }
}