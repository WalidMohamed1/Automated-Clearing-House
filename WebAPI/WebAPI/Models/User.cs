using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User_Status UserStatus { get; set; }
        public enum User_Status
        {
            Maker,
            Confirmer,
        }
        //public virtual Maker Maker { get; set; }
        //public virtual Confirmer Confirmer { get; set; }

    }
}