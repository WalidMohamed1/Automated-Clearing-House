using TestProject1.Mocks;
using WebAPI.Models;
using WebAPI.Repos;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        // bank Sender[of coprporate]
        // account number [coprporate]
        // bank Reciver[of employee]
        // account number [individual]
        // website where maker can create an empty batch.
        // maker can view the created batch.
        // Call to API -- integration test
        //unittes
        // add trx.
        // view trx.
        // submit to corporate confirmer.

        IBatchRepo batchRepo;
        ITrxRepo trxRepo;

        [TestMethod]
        public void BatchsConst_InflatedSuccess()
        {
            batchRepo = new BatchRepoMock();
            List<Batch> batches = batchRepo.GetBatches();

            Assert.AreEqual(1, batches[0].Id);
            //Assert.AreEqual(DateTime.Now, batches[0].CreationDate);
            // Data Time will always fail it is a logic!!
            Assert.AreEqual(8, batches[0].ApprovedTransactionCount);
            Assert.AreEqual(2, batches[0].RejectedTransactionCount);
            Assert.AreEqual(10, batches[0].TotalTransactionCount);
            Assert.AreEqual(4000, batches[0].TotalApprovedAmount);
            Assert.AreEqual(2000, batches[0].TotalRejectedAmount);
            Assert.AreEqual(6000, batches[0].TotalAmount);

        }

        [TestMethod]
        public void BatchConst_InflatedSuccess()
        {
            batchRepo = new BatchRepoMock();
            Batch batch = batchRepo.GetBatch(1);

            Assert.AreEqual(8, batch.ApprovedTransactionCount);

        }

        [TestMethod]
        public void MakerUserCanCreateBatch()
        {
            batchRepo = new BatchRepoMock();
            Batch batch = new Batch();
            batch.Id = 3;
            batch.CreationDate = DateTime.Now;
            batch.ApprovedTransactionCount = 3;
            batch.RejectedTransactionCount = 4;
            batch.TotalTransactionCount = 7;
            batch.TotalApprovedAmount = 2000;
            batch.TotalRejectedAmount = 1000;
            batch.TotalAmount = 3000;
            batchRepo.AddBatch(batch);

            Assert.AreEqual(3, batch.Id);
        }
        public void MakerUserCanUpdateBatch()
        {

        }
        public void MakerUserCanDeleteBatch()
        {

        }


        //-------------------------------------------------------------------


        [TestMethod]
        public void TrxsConst_InflatedSuccess()
        {
            trxRepo = new TrxRepoMock();
            List<Transaction> transactions = trxRepo.GetTrxs();

            Assert.AreEqual(1, transactions[0].Id);
            Assert.AreEqual(1, transactions[0].BatchId);
            Assert.AreEqual(2, transactions[0].RcvBankId);
            Assert.AreEqual("Bank Misr", transactions[0].RcvBankName);
            Assert.AreEqual(1234567891111, transactions[0].CoprorateAccountNumber);
            Assert.AreEqual(1234567892222, transactions[0].EmployeeAccountNumber);
            Assert.AreEqual(2000, transactions[0].Amount);
            Assert.AreEqual("usd", transactions[0].Currency);

        }

        [TestMethod]
        public void TrxConst_InflatedSuccess()
        {
            trxRepo = new TrxRepoMock();
            Transaction trx = trxRepo.GetTrx(1);

            Assert.AreEqual("usd", trx.Currency);

        }

        [TestMethod]
        public void MakerUserCanCreateTrx()
        {
            trxRepo = new TrxRepo();
            Transaction trx = new Transaction();
            trx.Id = 4;
            trx.BatchId = 2;
            trx.CreationDate = DateTime.Now;
            trx.RcvBankId = 1;
            trx.CoprorateAccountNumber = 1234567893333;
            trx.EmployeeAccountNumber = 1234567894444;
            trx.Amount = 5000;
            trx.Currency = "egp";
            trxRepo.AddTrx(trx);

            Assert.AreEqual(4, trx.Id);
        }
        public void MakerUserCanUpdateTrx()
        {

        }
        public void MakerUserCanDeleteTrx()
        {

        }
       
    }
}