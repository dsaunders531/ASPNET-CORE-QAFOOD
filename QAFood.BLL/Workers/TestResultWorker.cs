using QAFood.DAL;
using QAFood.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace QAFood.BLL.Workers
{
    /// <summary>
    /// Worker for TestResults.
    /// </summary>
    public class TestResultWorker
    {
        private IRepository<FoodParcel> FoodParcelRepository { get; set; }

        public TestResultWorker(IRepository<FoodParcel> foodParcelRepository)
        {
            this.FoodParcelRepository = foodParcelRepository;
        }

        public List<FoodParcel> GetFoodParcels()
        {
            return this.FoodParcelRepository.GetAll.ToList();
        }
    }
}
