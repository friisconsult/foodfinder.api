using System;

namespace FoodFinder.API.Model
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public long Version { get; set; }
        public DateTime Created { get; set; }

        public string Title { get; set; }
        public string Details { get; set; }


        public EntityBase()
        {
            Created = DateTime.UtcNow;
        }

    }
}