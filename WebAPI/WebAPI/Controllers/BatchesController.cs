using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Repos;

namespace WebAPI.Controllers
{
    public class BatchesController : ApiController
    {

        IBatchRepo repo = new BatchRepo();

        [HttpGet]
        public List<Batch> GetBatches()
        {
            return repo.GetBatches();
        }

        [HttpGet]
        public Batch GetBatch(int id)
        {
            return repo.GetBatch(id);
        }

        [HttpPost]
        public void AddBatch(Batch batch)
        {
            repo.AddBatch(batch);
        }

        [HttpPut]
        public void UpdateBatch(int id, Batch batch)
        {
            repo.UpdateBatch(id,batch);
        }

        [HttpDelete]
        public void DeleteBatch(int id)
        {
            repo.DeleteBatch(id);
        }

        [HttpPut]
        public void SubmitBatch(int id)
        {
            repo.SubmitBatch(id);
        }
        [HttpPut]
        public void AcceptBatch(int id)
        {
            repo.AcceptBatch(id);
        }
        [HttpPut]
        public void RejectBatch(int id)
        {
            repo.RejectBatch(id);
        }


        [HttpGet]
        public IEnumerable<Batch> GetPendingBatches()
        {
            return repo.GetPendingBatches();
        }

        [HttpGet]
        public IEnumerable<Batch> GetConfirmedBatches()
        {
            return repo.GetConfirmedBatches();
        }

        [HttpGet]
        public IEnumerable<Batch> GetReviewedBatches()
        {
            return repo.GetReviewedBatches();
        }

        [HttpGet]
        public IEnumerable<Batch> GetFinsishedBatches()
        {
            return repo.GetFinsishedBatches();
        }

    }
}
