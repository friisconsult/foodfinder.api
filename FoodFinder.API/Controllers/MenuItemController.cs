using System;
using System.Linq;
using FoodFinder.API.Authentication;
using FoodFinder.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ApplicationKeyAttributem))]
    public class MenuItemController : Controller
    {
        private readonly FoodFinderContext _context;

        public MenuItemController(FoodFinderContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult MenuItem([FromRoute] Guid id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
                return Ok(menuItem);

            return NotFound();
        }

        [HttpGet]
        public IActionResult MenuItems()
        {
            return Ok(_context.MenuItems);
        }

        [HttpGet("menu/{venueId}")]
        public IActionResult Menu([FromRoute] Guid venueId)
        {
            var menu = _context.Venues.Where(venue => venue.Id == venueId).Include(venue => venue.MenuItems);
            if (menu == null)
                return NotFound();

            return Ok(menu);
        }


        [HttpPost("menu/{venueId}")]

        public IActionResult PostMenuItem([FromRoute] Guid venueId, [FromBody] MenuItem menuItem)
        {
            var venue = _context.Venues.Find(venueId);
            if (venue == null)
                return NotFound();

            venue.MenuItems.Add(menuItem);

            _context.SaveChanges();

            return Created(menuItem.Id.ToString(), menuItem);
        }

    }
}