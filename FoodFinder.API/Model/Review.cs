using System;

namespace FoodFinder.API.Model
{
    public class Review:EntityBase
    {

        public int Stars { get; set; }
        public string Author { get; set; }
    }
}