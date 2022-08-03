using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Repos
{
    public class BatchRepo : IBatchRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void AddBatch(Batch batch)
        {
            db.batches.Add(batch);
            db.SaveChanges();
        }

        public List<Batch> GetBatches()
        {
            return db.batches.ToList();
        }

        public Batch GetBatch(int id)
        {
            return db.batches.Find(id);
        }

        public void UpdateBatch(int id, Batch batchChanges)
        {

            var oldBatch = db.batches.Find(id);

            oldBatch.CreationDate = batchChanges.CreationDate;
            oldBatch.ApprovedTransactionCount = batchChanges.ApprovedTransactionCount;
            oldBatch.RejectedTransactionCount = batchChanges.RejectedTransactionCount;
            oldBatch.TotalApprovedAmount = batchChanges.TotalApprovedAmount;
            oldBatch.TotalRejectedAmount = batchChanges.TotalRejectedAmount;
            oldBatch.TotalAmount = batchChanges.TotalAmount;  
            oldBatch.BatchStatus = batchChanges.BatchStatus;           
            oldBatch.transactions = batchChanges.transactions;

            db.SaveChanges();
        }

        public void DeleteBatch(int id)
        {
            Batch batch = db.batches.Find(id);
            if (batch != null) 
            { 
                db.batches.Remove(batch);
                db.SaveChanges();
            }
        }

        public void SubmitBatch(int id)
        {
            var oldBatch = db.batches.Find(id);
            var Stat = oldBatch.BatchStatus;
            if(Stat == Batch.Status.Created)
                oldBatch.BatchStatus = Batch.Status.Pending;
            db.SaveChanges();
        }
        public void AcceptBatch(int id)
        {
            var oldBatch = db.batches.Find(id);
            var Stat = oldBatch.BatchStatus;
            if (Stat == Batch.Status.Pending)
                oldBatch.BatchStatus = Batch.Status.Confirmed;
            else if (Stat == Batch.Status.Confirmed)
                oldBatch.BatchStatus = Batch.Status.Reviewed;
            else if (Stat == Batch.Status.Reviewed)
                oldBatch.BatchStatus = Batch.Status.Finished;
            db.SaveChanges();
        }
        public void RejectBatch(int id)
        {
            var oldBatch = db.batches.Find(id);
            var Stat = oldBatch.BatchStatus;
            if (Stat == Batch.Status.Pending)
                oldBatch.BatchStatus = Batch.Status.Created;
            if (Stat == Batch.Status.Confirmed)
                oldBatch.BatchStatus = Batch.Status.Pending;
            else if (Stat == Batch.Status.Reviewed)
                oldBatch.BatchStatus = Batch.Status.Confirmed;
            else if (Stat == Batch.Status.Finished)
                oldBatch.BatchStatus = Batch.Status.Reviewed;
            db.SaveChanges();
        }
        public IEnumerable<Batch> GetPendingBatches()
        {
            return db.batches.Where(b=>b.BatchStatus==Batch.Status.Pending).ToList();
        }

        public IEnumerable<Batch> GetConfirmedBatches()
        {
            return db.batches.Where(b => b.BatchStatus == Batch.Status.Confirmed).ToList();
        }

        public IEnumerable<Batch> GetReviewedBatches()
        {
            return db.batches.Where(b => b.BatchStatus == Batch.Status.Reviewed).ToList();
        }

        public IEnumerable<Batch> GetFinsishedBatches()
        {
            return db.batches.Where(b => b.BatchStatus == Batch.Status.Finished).ToList();
        }

    }
}