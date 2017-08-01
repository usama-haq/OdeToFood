﻿using System.ComponentModel.DataAnnotations;

namespace OdeToFood.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(255)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}