using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ACH.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Status")]
        public User_Status UserStatus { get; set; }
        public enum User_Status
        {
            Maker,
            Confirmer,
        }
    }
}