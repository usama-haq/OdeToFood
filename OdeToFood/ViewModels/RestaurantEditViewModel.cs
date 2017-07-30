using OdeToFood.Entities;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        [Display(Name = "Restaurant Name ")]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}