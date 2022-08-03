using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Repos;

namespace WebAPI.Controllers
{
    public class TransactionsController : ApiController
    {
        ITrxRepo repo = new TrxRepo();

        [HttpGet]
        public List<Transaction> GetTrxs()
        {
            return repo.GetTrxs();
        }
        [HttpGet]
        public Transaction GetTrx(int id)
        {
            return repo.GetTrx(id);
        }
        [HttpPost]
        public void AddTrx(Transaction trx)
        {
            repo.AddTrx(trx);
        }
        [HttpPut]
        public void UpdateTrx(int id, Transaction trx)
        {
            repo.UpdateTrx(id, trx);
        }
        [HttpDelete]
        public void DeleteTrx(int id)
        {
            repo.DeleteTrx(id);
        }
    }
}