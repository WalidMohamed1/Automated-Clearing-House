using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Repos
{
    public class TrxRepo : ITrxRepo

    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void AddTrx(Transaction trx)
        {
            db.transactions.Add(trx);
            db.SaveChanges();
        }

        public List<Transaction> GetTrxs()
        {
            //var trxs = db.transactions.Include(t => t.Batch);
            return db.transactions.ToList();
        }

        public Transaction GetTrx(int id)
        {
            return db.transactions.Find(id);
        }

        public void UpdateTrx(int id, Transaction trxChanges)
        {
            var oldTrx = db.transactions.Find(id);

            oldTrx.BatchId = trxChanges.BatchId;
            oldTrx.CreationDate = trxChanges.CreationDate;
            oldTrx.RcvBankId = trxChanges.RcvBankId;
            oldTrx.RcvBankName = trxChanges.RcvBankName;
            oldTrx.CoprorateAccountNumber = trxChanges.CoprorateAccountNumber;
            oldTrx.EmployeeAccountNumber = trxChanges.EmployeeAccountNumber;
            oldTrx.Amount = trxChanges.Amount;
            oldTrx.Currency = trxChanges.Currency;

            db.SaveChanges();
        }

        public void DeleteTrx(int id)
        {
            Transaction trx = db.transactions.Find(id);
            if (trx != null)
            {
                db.transactions.Remove(trx);
                db.SaveChanges();
            }
        }
  
    }
}