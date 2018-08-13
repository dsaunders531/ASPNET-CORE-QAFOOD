using QAFood.BLL.ViewModels;
using QAFood.BLL.Workers;
using QAFood.DAL.Models;
using QAFood.XUnitTests.TestRepositories;
using Xunit;

namespace QAFood.XUnitTests
{
    /// <summary>
    /// Tests for the ReviewWorker Class
    /// </summary>
    public class ReviewWorkerTests
    {
        /// <summary>
        /// Does saving a review work?
        /// </summary>
        [Fact]
        public void TestSavingReview()
        {
            // Create a review process model with a test value.
            ReviewProcessViewModel model = new ReviewProcessViewModel()
            { SelectedFoodItemId = 0, TestId = 0, FoodParcelId = 0, AromaValue = 3 };

            // Create a test repository.
            TestResultItemTestRepository resultItemTestRepository = new TestResultItemTestRepository();

            // Create a worker to process the request. Note only the repository I need is passed.
            ReviewWorker reviewWorker = new ReviewWorker(null, null, null, resultItemTestRepository, null);

            // Save the review.
            reviewWorker.SaveReview(model, "Test User");

            // Get the saved item.
            TestResultItem result = resultItemTestRepository.Get(0);

            // Check the value has been saved.
            Assert.True(result.Result == model.AromaValue, "The new aroma value was not saved.");
        }
    }
}
