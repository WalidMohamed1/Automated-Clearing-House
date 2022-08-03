using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        // Many To One
        public int BatchId { get; set; }
        //public virtual Batch Batch { get; set; } //Reference Navigation Property
        public DateTime CreationDate { get; set; }
        // Many to One
        public int RcvBankId { get; set; }
        public string RcvBankName { get; set; }
        public long CoprorateAccountNumber { get; set; }
        public long EmployeeAccountNumber { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        
        public Transaction()
        {
            CreationDate = DateTime.Now;
        }
    }
}