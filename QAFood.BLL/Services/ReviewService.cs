using QAFood.BLL.Workers;
using QAFood.DAL;
using QAFood.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace QAFood.BLL.Services
{
    /// <summary>
    /// The review service
    /// </summary>
    public sealed class ReviewService : ReviewWorker
    {
        private IRepository<FoodParcel> FoodParcelRepository { get; set; }

        public ReviewService(IRepository<FoodParcel> foodParcelRepository,
                                IRepository<FoodItem> foodItemRepository,
                                IRepository<TestResult> testResultRepository,
                                IRepository<TestResultItem> testResultItemRepository,
                                IRepository<TestItemCategory> testItemResultCategoryRepository) 
            : base(foodParcelRepository, foodItemRepository, testResultRepository, testResultItemRepository, testItemResultCategoryRepository)
        {
            this.FoodParcelRepository = foodParcelRepository;
        }

        public List<FoodParcel> GetFoodParcelsPaginated(int skip, int take)
        {
            return this.FoodParcelRepository.GetAll
                                              .OrderBy(o => o.PostedDate)
                                              .Skip(skip)
                                              .Take(take)
                                              .ToList();
        }
    }
}
