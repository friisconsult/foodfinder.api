﻿using System;
using System.Linq;
using FoodFinder.API.Authentication;
using FoodFinder.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("menu/{venueId}")]
        public IActionResult Menu([FromRoute] Guid venueId)
        {
            var menu = _context.MenuItems.Where(m => m.VenueId == venueId).OrderBy(m => m.Title);
            if (menu == null)
                return NotFound();

            return Ok(menu);
        }


        [HttpPost]
        public IActionResult PostMenuItem([FromBody] MenuItem menuItem)
        {
            menuItem.Venue = _context.Venues.Find(menuItem.VenueId);
            if (menuItem.Venue == null)
                return BadRequest($"No venues with id {menuItem.VenueId}");

            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();

            return Created(menuItem.Id.ToString(), menuItem);
        }

    }
}