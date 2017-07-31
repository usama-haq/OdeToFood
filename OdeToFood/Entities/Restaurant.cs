using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdeToFood.Entities
{
    public enum CuisineType
    {
        None,
        Italian,
        French,
        Japanese,
        American
    }

    public class Restaurant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(80)]
        [DataType(DataType.Text)]
        [Display(Name = "Restaurant Name ")]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}