using WebAPI.Models;
using WebAPI.Repos;

namespace TestProject1.Mocks
{
    public class BatchRepoMock : IBatchRepo

    {
        public List<Batch> batches { get ; set; }
        
        public BatchRepoMock()
        {
            batches = new() {
                new Batch {
                Id=1 ,
                CreationDate = DateTime.Now,
                ApprovedTransactionCount = 8,
                RejectedTransactionCount = 2,
                TotalTransactionCount = 10,
                TotalApprovedAmount = 4000,
                TotalRejectedAmount = 2000,
                TotalAmount = 6000,
                },

                new Batch {
                Id=2 ,
                CreationDate= DateTime.Now,
                ApprovedTransactionCount = 3,
                RejectedTransactionCount = 9,
                TotalTransactionCount = 12,
                TotalApprovedAmount = 5000,
                TotalRejectedAmount = 4000,
                TotalAmount = 9000,
                }
            };

        }

        public void AddBatch(Batch batch)
        {
            batch.Id = batches.Max(b => b.Id) +1; //id ++
            batches.Add(batch);
        }

        public List<Batch> GetBatches()
        {
            return batches;
        }      

        public Batch GetBatch(int id)
        {
            var batch = batches.SingleOrDefault(x => x.Id == id);
            return batch;
        }

        public void UpdateBatch(int id, Batch batchChanges)
        {
            var batch = GetBatch(id);
            batches[id] = batchChanges;
        }

        public void DeleteBatch(int id)
        {
            var batch = batches.SingleOrDefault(x => x.Id == id);
            if (batch != null)
            {
                batches.Remove(batch); 
            }
        }
    }
}
