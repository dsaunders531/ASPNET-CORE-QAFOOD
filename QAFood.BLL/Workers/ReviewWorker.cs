using QAFood.BLL.ViewModels;
using QAFood.DAL;
using QAFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QAFood.BLL.Workers
{
    /// <summary>
    /// Worker for Reviews.
    /// </summary>
    public class ReviewWorker
    {
        private IRepository<FoodParcel> FoodParcelRepository { get; set; }
        private IRepository<FoodItem> FoodItemRepository { get; set; }
        private IRepository<TestResult> TestResultRepository { get; set; }
        private IRepository<TestResultItem> TestResultItemRepository { get; set; }
        private IRepository<TestItemCategory> TestItemResultCategoryRepository { get; set; }

        public ReviewWorker(IRepository<FoodParcel> foodParcelRepository,
                                IRepository<FoodItem> foodItemRepository,
                                IRepository<TestResult> testResultRepository,
                                IRepository<TestResultItem> testResultItemRepository,
                                IRepository<TestItemCategory> testItemResultCategoryRepository)
        {
            this.FoodParcelRepository = foodParcelRepository;
            this.FoodItemRepository = foodItemRepository;
            this.TestResultRepository = testResultRepository;
            this.TestResultItemRepository = testResultItemRepository;
            this.TestItemResultCategoryRepository = testItemResultCategoryRepository;
        }

        public List<FoodParcel> GetFoodParcels()
        {
            return this.FoodParcelRepository.GetAll.OrderBy(o => o.PostedDate).ToList();
        }

        public FoodParcel GetFoodParcel(long parcelId)
        {
            return this.FoodParcelRepository.Get(parcelId);
        }

        /// <summary>
        /// Get a food parcel for a user.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="foodParcelId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public TestResult GetTestResult(long foodParcelId, string username)
        {
            return (from TestResult t in this.TestResultRepository.GetAll where t.FoodParcelId == foodParcelId && t.UserName == username select t).FirstOrDefault();
        }

        /// <summary>
        /// Create test records for a user and food parcel.
        /// </summary>
        /// <param name="foodParcel"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public TestResult CreateTest(FoodParcel foodParcel, string username)
        {
            List<TestItemCategory> measurementCategories = (from TestItemCategory l in this.TestItemResultCategoryRepository.GetAll
                                                            select l).OrderBy(o => o.Value).ToList();

            List<FoodItem> selectedFoodItems = foodParcel.FoodItems;

            TestResult test = new TestResult()
            {
                FoodParcelId = foodParcel.Id,
                TestDate = DateTime.Now,
                Name = foodParcel.Name + " Review",
                UserName = username
            };

            this.TestResultRepository.Create(test);
            this.TestResultRepository.Save();

            // Add the food items to the test
            foreach (FoodItem item in selectedFoodItems)
            {
                foreach (TestItemCategory category in measurementCategories)
                {
                    TestResultItem testResultItem = new TestResultItem()
                    {
                        CategoryId = category.Id,
                        FoodItemId = item.Id,
                        Result = 0,
                        TestResultId = test.Id
                    };
                    this.TestResultItemRepository.Create(testResultItem);
                }
            }
            this.TestResultItemRepository.Save();

            test = this.GetTestResult(foodParcel.Id, username); // Check the new item has been saved properly

            return test;
        }

        /// <summary>
        /// Create a reviewprocessviewmodel using a reviewstart model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public ReviewProcessViewModel AssembleReviewProcessViewModel(ReviewStartResultViewModel model, string username)
        {
            ReviewProcessViewModel result = new ReviewProcessViewModel()
            {
                FoodParcelId = model.FoodParcelId,
                SelectedFoodItemId = model.SelectedFoodItemId,
                TestId = model.TestId
            };

            FoodParcel foodParcel = this.FoodParcelRepository.Get(result.FoodParcelId);
            result.FoodParcelName = foodParcel.Name;

            FoodItem foodItem = this.FoodItemRepository.Get(result.SelectedFoodItemId);
            result.FoodItemName = foodItem.Description;

            TestResult testResult = this.TestResultRepository.Get(result.TestId);
            result.TestName = testResult.Name;

            // Get the test items using PLINQ - it may run a bit faster when there are lots of test submissions
            List<TestResultItem> testResultItems = (from TestResultItem t in this.TestResultItemRepository.GetAll.AsParallel().WithDegreeOfParallelism(2)
                                                    where t.FoodItemId == result.SelectedFoodItemId
                                                     && t.TestResultId == result.TestId
                                                     && t.TestResult.UserName == username
                                                    select t).ToList();

            // The disadvantage of using hard-coded values is that it is difficult to change and add new things later 
            // without changing code, recompiling, testing and deploying.
            result.PresentationValue = (from TestResultItem t in testResultItems where t.Category.Value == "Presentation" select t.Result).FirstOrDefault();
            result.TextureValue = (from TestResultItem t in testResultItems where t.Category.Value == "Texture" select t.Result).FirstOrDefault();
            result.AromaValue = (from TestResultItem t in testResultItems where t.Category.Value == "Aroma" select t.Result).FirstOrDefault();
            result.FlavourValue = (from TestResultItem t in testResultItems where t.Category.Value == "Flavour" select t.Result).FirstOrDefault();

            return result;
        }

        public ReviewProcessViewModel AssembleReviewProcessViewModel(long foodParcelId, long selectedFoodItemId, long testId, string username)
        {
            ReviewStartResultViewModel model = new ReviewStartResultViewModel() { FoodParcelId = foodParcelId, SelectedFoodItemId = selectedFoodItemId, TestId = testId };

            return this.AssembleReviewProcessViewModel(model, username);
        }

        /// <summary>
        /// Save the review using the review process model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="username"></param>
        public void SaveReview(ReviewProcessViewModel model, string username)
        {
            // Match the values in the model with database using PLINQ
            List<TestResultItem> testResultItems = (from TestResultItem t in this.TestResultItemRepository.GetAll.AsParallel().WithDegreeOfParallelism(2)
                                                    where t.FoodItemId == model.SelectedFoodItemId
                                                     && t.TestResultId == model.TestId
                                                     && t.TestResult.UserName == username
                                                    select t).ToList();

            // Update 
            foreach (TestResultItem item in testResultItems)
            {
                switch (item.Category.Value)
                {
                    case "Presentation":
                        item.Result = model.PresentationValue;
                        break;
                    case "Texture":
                        item.Result = model.TextureValue;
                        break;
                    case "Aroma":
                        item.Result = model.AromaValue;
                        break;
                    case "Flavour":
                        item.Result = model.FlavourValue;
                        break;
                    default:
                        break;
                }

                this.TestResultItemRepository.Update(item);
            }

            // Save
            this.TestResultItemRepository.Save();
        }
    }
}
