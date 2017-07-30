using OdeToFood.Entities;
using System.Collections.Generic;

namespace OdeToFood.Contracts
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();

        Restaurant Get(int id);
    }
}