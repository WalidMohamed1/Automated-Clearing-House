using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Repos
{
    public interface ITrxRepo
    {
        void AddTrx(Transaction transaction);
        List<Transaction> GetTrxs();
        Transaction GetTrx(int id);
        void UpdateTrx(int id, Transaction transaction);
        void DeleteTrx(int id);
    }
}