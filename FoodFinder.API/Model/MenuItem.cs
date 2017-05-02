using System;

namespace FoodFinder.API.Model
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public long Version { get; set; }
        public DateTime Created { get; set; }

        public string Title { get; set; }
        public string Detail { get; set; }

        public MenuItemType Type { get; set; }
        public double Price { get; set; }

        public MenuItem()
        {
            Created = DateTime.UtcNow;
            Version = 0;
        }
    }

    public enum MenuItemType
    {
        Starter,
        Main,
        SideDish,
        Dessert,
        SoftDrink,
        AlcoholicDrink
    }
}