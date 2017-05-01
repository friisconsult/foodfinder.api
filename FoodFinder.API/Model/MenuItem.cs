using System;

namespace FoodFinder.API.Model
{
    public class MenuItem : EntityBase
    {
        public MenuItemType Type { get; set; }
        public double Price { get; set; }
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