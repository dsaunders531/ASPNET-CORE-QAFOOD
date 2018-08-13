using QAFood.BLL.Workers;
using QAFood.DAL;
using QAFood.DAL.Models;

namespace QAFood.BLL.Services
{
    /// <summary>
    /// The review service
    /// </summary>
    public sealed class ReviewService : ReviewWorker
    {
        public ReviewService(IRepository<FoodParcel> foodParcelRepository,
                                IRepository<FoodItem> foodItemRepository,
                                IRepository<TestResult> testResultRepository,
                                IRepository<TestResultItem> testResultItemRepository,
                                IRepository<TestItemCategory> testItemResultCategoryRepository) 
            : base(foodParcelRepository, foodItemRepository, testResultRepository, testResultItemRepository, testItemResultCategoryRepository)
        {       
        }
    }
}
