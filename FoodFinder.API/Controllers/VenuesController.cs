using System;
using FoodFinder.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder.API.Authentication;

namespace FoodFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ApplicationKeyAttributem))]
    public class VenuesController : Controller
    {
        private readonly FoodFinderContext _context;

        public VenuesController(FoodFinderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// return all the venues
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Venues()
        {
            return Ok(_context.Venues.ToList());
        }

        /// <summary>
        /// return specific venue, including menu and reviews
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [HttpGet("{venueId}")]
        public async Task<IActionResult> Venue([FromRoute]Guid venueId)
        {

            var venue = await _context
                .Venues.Include(v => v.MenuItems)
                .Include(v => v.Reviews)
                .FirstOrDefaultAsync(v => v.Id == venueId);

            if (venue != null)
                return Ok(venue);

            return NotFound($"No venue with id {venueId}");
        }

        /// <summary>
        /// Create a new venue, the new venue can include menuitems and reviews
        /// </summary>
        /// <param name="venues"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ApplicationKeyAttributem))]
        public IActionResult PostVenue([FromBody] Venue[] venues)
        {
            foreach (var venue in venues)
            {
                 _context.Venues.Add(venue);
                            _context.SaveChanges();
            }
            return Created(venues.First().Id.ToString(), venues.FirstOrDefault());
        }


    }


}