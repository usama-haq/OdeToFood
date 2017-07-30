using OdeToFood.Models;
using System.Collections.Generic;

namespace OdeToFood.Contracts
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }
}