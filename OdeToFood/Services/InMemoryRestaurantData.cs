using OdeToFood.Contracts;
using OdeToFood.Models;
using System;
using System.Collections.Generic;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant{Id = 1, Name="The House of Kobe" },
                new Restaurant{Id = 2, Name = "LJ's and the Kat" },
                new Restaurant {Id = 3, Name = "King's Contrivance" }
            };
        }

        IEnumerable<Restaurant> IRestaurantData.GetAll()
        {
            return _restaurants;
        }
    }
}