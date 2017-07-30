using OdeToFood.Contracts;
using OdeToFood.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private static List<Restaurant> _restaurants;

        static InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant{Id = 1, Name="The House of Kobe" },
                new Restaurant{Id = 2, Name = "LJ's and the Kat" },
                new Restaurant {Id = 3, Name = "King's Contrivance" }
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        IEnumerable<Restaurant> IRestaurantData.GetAll()
        {
            return _restaurants;
        }
    }
}