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
    public class MenuItemController : Controller
    {
        private readonly FoodFinderContext _context;

        public MenuItemController(FoodFinderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get specific menu item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MenuItem</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> MenuItem([FromRoute] Guid id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
                return Ok(menuItem);

            return NotFound();
        }

        /// <summary>
        /// Get Menu items for a specific venue
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [HttpGet("menu/{venueId}")]
        public IActionResult Menu([FromRoute] Guid venueId)
        {
            var menu = _context.Venues
                .Include(venue => venue.MenuItems)
                .FirstOrDefault(v => v.Id == venueId)
                .MenuItems
                .OrderBy(m => m.Type)
                .ToList();

            if (menu == null)
                return NotFound();

            return Ok(menu);
        }


        /// <summary>
        /// Create and attach a menuiten to a venue
        /// </summary>
        /// <param name="venueId"></param>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        [HttpPost("menu/{venueId}")]
        public async Task<IActionResult> PostMenuItem([FromRoute] Guid venueId, [FromBody] MenuItem menuItem)
        {
            var venue = await _context.Venues.Include(v => v.MenuItems).FirstOrDefaultAsync(v => v.Id == venueId);

            if (venue == null)
                return NotFound();

            venue.MenuItems.Add(menuItem);

            _context.SaveChanges();

            return Created(menuItem.Id.ToString(), menuItem);
        }

    }
}