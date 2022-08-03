using ACH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace ACH.Controllers
{
    public class ConfirmerController : Controller
    {
        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
        HttpClient clinet; 
        public ConfirmerController()
        {
            clinet = new HttpClient();
            clinet.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            if (Session["Confirmer"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                List<Batch> batches = new List<Batch>();
                HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/batches/GetPendingBatches").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    batches = JsonConvert.DeserializeObject<List<Batch>>(data);
                }
                return View(batches);
            }
        }

        public ActionResult UpdateBatch(int? id)
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
        public ActionResult UpdateBatch(Batch batch)
        {
            string data = JsonConvert.SerializeObject(batch);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clinet.PutAsync(clinet.BaseAddress + "/batches/UpdateBatch/" + batch.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Confirmer");
            }
            return View("UpdateBatch", batch);
        }

        public ActionResult AcceptBatch(int id)
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
        public ActionResult AcceptBatch(Batch batch)
        {
            string data = JsonConvert.SerializeObject(batch);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clinet.PutAsync(clinet.BaseAddress + "/batches/AcceptBatch/" + batch.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Confirmer");
            }
            return View("AcceptBatch", batch);
        }

        public ActionResult RejectBatch(int id)
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
        public ActionResult RejectBatch(Batch batch)
        {
            string data = JsonConvert.SerializeObject(batch);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clinet.PutAsync(clinet.BaseAddress + "/batches/RejectBatch/" + batch.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Confirmer");
            }
            return View("RejectBatch", batch);
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
    }
}