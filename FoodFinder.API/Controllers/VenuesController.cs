using System;
using FoodFinder.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        [HttpGet]
        public IActionResult Venues()
        {
            return Ok(_context.Venues.Include(venue => venue.MenuItems).Include(venue => venue.Reviews));
        }

        [HttpGet("{venueId}")]
        public IActionResult Venue([FromRoute]Guid venueId)
        {

            var venue = _context.Venues.Find(venueId);
            if (venue != null)
                return Ok(venue);

            return NotFound($"No venue with id {venueId}");
        }

        [HttpPost]
        [ServiceFilter(typeof(ApplicationKeyAttributem))]
        public IActionResult PostVenue([FromBody] Venue venue)
        {
            _context.Venues.Add(venue);
            _context.SaveChanges();
            return Created(venue.Id.ToString(), venue);
        }


    }


}