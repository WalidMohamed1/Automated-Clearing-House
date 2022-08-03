using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACH.Models
{
    public class Batch
    {
        public int Id { get; set; }
        // Many to One
        public int MakerId { get; set; }
        // Many to One
        public int ConfirmerId { get; set; }
        public DateTime CreationDate { get; set; }
        public int ApprovedTransactionCount { get; set; }
        public int RejectedTransactionCount { get; set; }
        public int TotalApprovedAmount { get; set; }
        public int TotalRejectedAmount { get; set; }
        public int TotalAmount { get; set; }
        // Many to One
        public int SenderBankId { get; set; }
        public string SenderBankName { get; set; }
        // One to Many
        public List<Transaction> transactions { get; set; }
        public Status BatchStatus { get; set; }
        public enum Status
        {
            Created,
            Pending,
            Confirmed,
            Reviewed,
            Finished
        }
        public Batch()
        {
            MakerId = 1;
            ConfirmerId = 1;
            CreationDate = DateTime.Now;
            BatchStatus = Status.Created;
            SenderBankId = 1;
            SenderBankName = "BanqueMisr";
        }
     
    }
}