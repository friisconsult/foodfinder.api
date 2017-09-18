using System;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder.API.Authentication;
using FoodFinder.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Get a specific review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Review([FromRoute] Guid id)
        {
            var comment = await _context.Reviews.FindAsync(id);
            if (comment != null)
                return Ok(comment);
            else
                return NotFound($"No review with reviewId {id} was found in the dataase");
        }


        /// <summary>
        /// Get reviews for a venue
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [HttpGet("venue/{venueId}")]
        public IActionResult ReviewFor([FromRoute] Guid venueId)
        {
            var reviews = _context.Venues
                .Include(v => v.Reviews)
                .FirstOrDefault(v => v.Id == venueId)
                .Reviews
                .ToList();

            if (reviews != null)
                return Ok(reviews);

            return NotFound($"The venue with venueId:{venueId.ToString()} do not have any review at the moment");
        }


        /// <summary>
        /// Save review for a venue
        /// </summary>
        /// <param name="venueId"></param>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost("{venueId}")]
        [ServiceFilter(typeof(ApplicationKeyAttributem))]
        public async Task<IActionResult> PostReview([FromRoute] Guid venueId, [FromBody] Review review)
        {
            var venue = await _context.Venues
                .Include(v => v.Reviews)
                .FirstOrDefaultAsync(v => v.Id == venueId);

            if (venue == null)
                return BadRequest($"There is no venue with venueId:{venueId}");

            venue.Reviews.Add(review);
            _context.SaveChanges();

            return Created(review.Id.ToString(), review);
        }
    }
}