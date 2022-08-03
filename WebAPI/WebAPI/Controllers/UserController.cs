using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Context;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult Register(UserRegisterDTO request) 
        {
            if (db.users.Any(u => u.Email == request.Email))
            {
                return BadRequest("Email already exists.");
            }
            var user = new User()
            {
                Email = request.Email,
                Password = request.Password,
                UserStatus = (User.User_Status)request.UserStatus,
            };
            db.users.Add(user);
            db.SaveChanges();
            return Ok("User successfully created!");
        }

        [HttpPost]
        public IHttpActionResult Login(UserLoginDTO request)
        {
            var maker = db.users.Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password) && u.UserStatus ==Models.User.User_Status.Maker).ToList();
            var confirmer = db.users.Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password) && u.UserStatus ==Models.User.User_Status.Confirmer).ToList();
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            if(maker.Count > 0)
            {
                result.Add("Maker", true);
                return Ok<Dictionary<string,bool>>(result);
                
            }
            else if(confirmer.Count > 0)
            {
                result.Add("Maker", false);
                return Ok<Dictionary<string, bool>>(result);
            }
            return Unauthorized();
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return db.users.ToList();
        }


    }
}