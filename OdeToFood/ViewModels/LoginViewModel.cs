using System.ComponentModel.DataAnnotations;

namespace OdeToFood.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(255)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Rememver Me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}