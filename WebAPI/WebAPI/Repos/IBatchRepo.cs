using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Repos
{
    public interface IBatchRepo
    {
        void AddBatch(Batch batch);
        List<Batch> GetBatches();
        Batch GetBatch(int id);
        void UpdateBatch(int id, Batch batch);
        void DeleteBatch(int id);


        IEnumerable<Batch> GetPendingBatches();
        IEnumerable<Batch> GetConfirmedBatches();
        IEnumerable<Batch> GetReviewedBatches();
        IEnumerable<Batch> GetFinsishedBatches();

        void SubmitBatch(int id);
        void AcceptBatch(int id);
        void RejectBatch(int id);

    }
}
