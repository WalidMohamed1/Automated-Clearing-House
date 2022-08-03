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
    public class MakerController : Controller
    {
        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
        HttpClient clinet; //To consume api (send request and receive response).
        public MakerController()
        {
            clinet = new HttpClient();
            clinet.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            if (Session["Maker"] == null)
            {
                return RedirectToAction("Login", "User");

            }
            else
            {
                List<Batch> batches = new List<Batch>();
                // GetAsync used to send Http GET request
                HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/batches/GetBatches").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result; //ReadAsStringAsync used to deserialize Json to obj
                    batches = JsonConvert.DeserializeObject<List<Batch>>(data); // To Deserilize 
                }
                return View(batches);
            }
        }


        public ActionResult GetBatch(int id)
        {
            Batch batch = new Batch();
            // GetAsync used to send Http GET request
            HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/batches/GetBatch/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result; //ReadAsStringAsync used to deserialize Json to obj
                batch = JsonConvert.DeserializeObject<Batch>(data); // To Deserilize 
            }
            return View("GetBatch", batch); //To render model into view
        }

        public ActionResult AddBatch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBatch(Batch batch)
        {
            string data = JsonConvert.SerializeObject(batch);
            //Serilize to convert obj to string(JSON) so it will store easily.
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json"); //?
            // GetAsync used to send Http GET request
            HttpResponseMessage response = clinet.PostAsync(clinet.BaseAddress + "/batches/AddBatch/" + batch.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Maker");
            }
            return View("AddBatch", batch);
        }


        public ActionResult UpdateBatch(int id)
        {
            Batch batch = new Batch();
            HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/batches/GetBatch/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                batch = JsonConvert.DeserializeObject<Batch>(data);
            }
            return View(batch);
        }

        public ActionResult SubmitBatch(int id)
        {
            Batch batch = new Batch();
            HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/batches/GetBatch/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                batch = JsonConvert.DeserializeObject<Batch>(data);
            }
            return View(batch);
        }


        [HttpPost]
        public ActionResult SubmitBatch(Batch batch)
        {
            string data = JsonConvert.SerializeObject(batch);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clinet.PutAsync(clinet.BaseAddress + "/batches/SubmitBatch/" + batch.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Maker");
            }
            return View("SubmitBatch", batch);
        }
        // Create an action for getting data by id for delete
        public ActionResult DeleteBatch(int? id)
        {
            Batch batch = new Batch();
            HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/batches/GetBatch/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                batch = JsonConvert.DeserializeObject<Batch>(data);
            }
            return View(batch);
        }
        [HttpPost]
        public ActionResult DeleteBatch(int id)
        {
            var response = clinet.DeleteAsync(clinet.BaseAddress + "/batches/DeleteBatch/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Maker");
            }
            return View("DeleteBatch");
        }
    }
}