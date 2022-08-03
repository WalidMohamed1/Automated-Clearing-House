using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Dtos
{
    public class UserRegisterDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public User_Status UserStatus { get; set; }
        public enum User_Status
        {
            Maker,
            Confirmer,
        }
    }
}