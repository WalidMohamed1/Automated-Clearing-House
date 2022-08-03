using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repos;

namespace TestProject1.Mocks
{
    public class TrxRepoMock : ITrxRepo
    {
        public List<Transaction> transactions { get; set; }
        public TrxRepoMock()
        {
            transactions = new List<Transaction>
            {
                new Transaction {
                Id=1 ,
                BatchId=1 ,
                CreationDate = DateTime.Now,
                RcvBankId = 2,
                RcvBankName = "Bank Misr",
                CoprorateAccountNumber = 1234567891111,
                EmployeeAccountNumber = 1234567892222,
                Amount = 2000,
                Currency = "usd",
                },

                new Transaction {
                Id=2 ,
                BatchId=1 ,
                CreationDate = DateTime.Now,
                RcvBankId = 3,
                RcvBankName = "Al-Ahly Bank",
                CoprorateAccountNumber = 1234567891111,
                EmployeeAccountNumber = 1234567892222,
                Amount = 8000,
                Currency = "egp",
                },

                new Transaction {
                Id=3 ,
                BatchId=1 ,
                CreationDate = DateTime.Now,
                RcvBankId = 2,
                RcvBankName = "Alex Bank",
                CoprorateAccountNumber = 1234567891111,
                EmployeeAccountNumber = 1234567892222,
                Amount = 200000,
                Currency = "sar",
                },

                new Transaction {
                Id=4 ,
                BatchId=2 ,
                CreationDate = DateTime.Now,
                RcvBankId = 2,
                RcvBankName = "Saib Bank",
                CoprorateAccountNumber = 1234567891111,
                EmployeeAccountNumber = 1234567892222,
                Amount = 2000,
                Currency = "eur",
                },
            };
            //AddTrx(new Transaction());
        }

        public void AddTrx(Transaction trx)
        {
            transactions.Add(trx);
        }

        public List<Transaction> GetTrxs()
        {
            return transactions;
        }

        public Transaction GetTrx(int id)
        {
            var trx = transactions.SingleOrDefault(x => x.Id == id);
            return trx;
        }

        public void UpdateTrx(Transaction trx)
        {
            var index = transactions.FindIndex(x => x.Id == trx.Id);
            if (index == -1)
                return;
            transactions[index] = trx;
        }

        public void DeleteTrx(int id)
        {
            var batch = GetTrx(id);
            if (batch is null)
                return;
            transactions.Remove(batch);
        }
    }
}
