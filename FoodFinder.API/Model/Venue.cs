﻿using System.Collections.Generic;
using System;

namespace FoodFinder.API.Model
{
    public class Venue
    {
        public Guid Id { get; set; }
        public long Version { get; set; }
        public DateTime Created { get; set; }

        public string Title { get; set; }
        public string Detail { get; set; }
        public FoodType Type { get; set; } = FoodType.Other;
        public string LogoImaageUrl { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;

        public double PriceLevel { get; set; } = 50;

        public  ICollection<MenuItem> MenuItems { get; set; }
        public   ICollection<Review> Reviews { get; set; }

        public Venue()
        {
            Version = 0;
            Created = DateTime.UtcNow;
        }
    }

    public enum FoodType
    {
        Other,
        FastFood,
        Indian,
        Mexican,
        French,
        MiddelEast,
        African,
        European,
        American,
        Vegetarian,
        LocalFood
    }
}