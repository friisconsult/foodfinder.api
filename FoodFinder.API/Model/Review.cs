using System;

namespace FoodFinder.API.Model
{
    public class Review
    {
        public Guid Id { get; set; }
        public long Version { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
        public string Owner { get; set; }

        public string Title { get; set; }
        public string Detail { get; set; }

        public int Stars { get; set; }
        public string Author { get; set; }

        public Review()
        {
            Version = 0;
            Created = DateTime.UtcNow;

        }
    }
}