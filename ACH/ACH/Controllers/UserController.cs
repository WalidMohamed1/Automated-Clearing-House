using ACH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ACH.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
        HttpClient clinet;
        public UserController()
        {
            clinet = new HttpClient();
            clinet.BaseAddress = baseAddress;
        }
        public ActionResult Register()
        {
            if (Session["Maker"] != null)
            {
                return RedirectToAction("Index", "Maker", new { Maker = Session["Maker"].ToString() });
            }
            if (Session["Confirmer"] != null)
            {
                return RedirectToAction("Index", "Confirmer", new { Confirmer = Session["Confirmer"].ToString() });
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clinet.PostAsync(clinet.BaseAddress + "/user/Register/" + user.Id, content).Result;
            if(user.Email == null)
            {
                ModelState.AddModelError("Email", "Email cannot be null !");
                return View();
            }
            else if (user.Password == null)
            {
                ModelState.AddModelError("Password", "Password cannot be null !");
                return View();
            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "User");
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ModelState.Clear();
                ModelState.AddModelError("Email", "Email already exists.");
                return View();
            }
            else
                return View();

        }

        public ActionResult Login()
        {
            if (Session["Maker"] != null)
            {
                return RedirectToAction("Index", "Maker", new { Maker = Session["Maker"].ToString() });
            }
            if (Session["Confirmer"] != null)
            {
                return RedirectToAction("Index", "Confirmer", new { Confirmer = Session["Confirmer"].ToString() });
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clinet.PostAsync(clinet.BaseAddress + "/user/Login/" + user.Id, content).Result;
            Dictionary<string, bool> obj = response.Content.ReadAsAsync<Dictionary<string, bool>>().Result;
            if (response.IsSuccessStatusCode)
            {
                if (obj["Maker"].Equals(true))
                {
                    Session["Maker"] = user.Email;
                    return RedirectToAction("Index", "Maker", new {Maker = user.Email});
                }
                else
                {
                    Session["Confirmer"] = user.Email;
                    return RedirectToAction("Index", "Confirmer", new { Confirmer = user.Email});
                }

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ModelState.Clear();
                ModelState.AddModelError(String.Empty, "Email or Password is incorrect!");
                return View();
            }
            
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }
    }
}