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
    public class TransactionsController : Controller
    {
        // GET: Transactions
        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
        HttpClient clinet;
        public TransactionsController()
        {
            clinet = new HttpClient();
            clinet.BaseAddress = baseAddress;
        }

        public ActionResult GetTrx(int id)
        {
            Transaction trx = new Transaction();
            HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/transactions/GetTrx/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result; 
                trx = JsonConvert.DeserializeObject<Transaction>(data); 
            }
            return View("GetTrx", trx); 
        }
        public ActionResult AddTrx()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTrx(Transaction trx)
        {
            string data = JsonConvert.SerializeObject(trx);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clinet.PostAsync(clinet.BaseAddress + "/transactions/AddTrx/" + trx.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetBatch"+ "/" + trx.BatchId, "Maker");
            }
            return View("AddTrx", trx);
        }


        public ActionResult UpdateTrx(int id)
        {
            Transaction trx = new Transaction();
            HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/transactions/GetTrx/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                trx = JsonConvert.DeserializeObject<Transaction>(data);
            }
            return View(trx);
        }
        [HttpPost]
        public ActionResult UpdateTrx(Transaction trx)
        {
            string data = JsonConvert.SerializeObject(trx);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json"); 
            HttpResponseMessage response = clinet.PutAsync(clinet.BaseAddress + "/transactions/UpdateTrx/" + trx.Id, content).Result;
            if (response.IsSuccessStatusCode)
            { 
                if (Session["Maker"] != null)
                    return RedirectToAction("GetBatch" + "/" + trx.BatchId, "Maker");
                if (Session["Confirmer"] != null)
                    return RedirectToAction("GetBatch" + "/" + trx.BatchId, "Confirmer");
            }
            return View("UpdateTrx", trx);
        }


        public ActionResult DeleteTrx(int? id)
        {
            Transaction trx = new Transaction();
            HttpResponseMessage response = clinet.GetAsync(clinet.BaseAddress + "/transactions/GetTrx/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                trx = JsonConvert.DeserializeObject<Transaction>(data);
            }
            return View(trx);
        }
        [HttpPost]
        public ActionResult DeleteTrx(int id)
        {
            var response = clinet.DeleteAsync(clinet.BaseAddress + "/transactions/DeleteTrx/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetBatch" + "/" + 1, "Maker");
            }
            return View("DeleteBatch");
        }



    }
}
