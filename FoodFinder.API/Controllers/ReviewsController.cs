using System;
using System.Linq;
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


        [HttpGet("venue/{id}")]
        public IActionResult ReviewFor([FromRoute] Guid id)
        {
//            var reviews = _context.Reviews.Where(r => r.VenueId == id);
//            if (reviews != null)
//                return Ok(reviews);

            return NotFound($"The venue with id:{id.ToString()} do not have any review at the moment");

        }


        [HttpPost("{venueId}")]
        [ServiceFilter(typeof(ApplicationKeyAttributem))]
        public IActionResult PostReview([FromRoute] Guid venueId, [FromBody] Review review)
        {
            var venue = _context.Venues.Find(venueId);
            if (venue == null)
                return BadRequest($"There is no venue with id:{venueId}");

            venue.Reviews.Add(review);
            _context.SaveChanges();

            return Created(review.Id.ToString(), review);
        }
    }
}