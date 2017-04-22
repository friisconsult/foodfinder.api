using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
namespace FoodFinder.API.Model
{
    public sealed class FoodFinderContext : DbContext
    {
        public DbSet<Venue> Venues { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public FoodFinderContext(DbContextOptions<FoodFinderContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}