using QAFood.BLL.Workers;
using QAFood.DAL;
using QAFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QAFood.BLL.Services
{
    /// <summary>
    /// The test result service.
    /// </summary>
    public sealed class TestResultService : TestResultWorker
    {
        public TestResultService(IRepository<FoodParcel> foodParcelRepository) : base(foodParcelRepository)
        {           
        }

        /// <summary>
        /// Get the list of all completed tests in a food parcel.
        /// </summary>
        /// <param name="foodParcel"></param>
        /// <returns></returns>
        public List<TestResultItem> CompletedTests(FoodParcel foodParcel)
        {
            List<TestResultItem> result = new List<TestResultItem>();

            if (foodParcel.TestResults != null)
            {
                foreach (TestResult item in foodParcel.TestResults)
                {
                    result.AddRange(from TestResultItem t in item.TestResultItems where t.Result > 0 select t);
                }
            }

            return result.OrderBy(f => f.FoodItem.Description).ToList();
        }

        /// <summary>
        /// Count the reviews for a food item.
        /// </summary>
        /// <param name="foodItem"></param>
        /// <param name="testResultItems"></param>
        /// <returns></returns>
        public long ReviewCountForFoodItem(FoodItem foodItem, List<TestResultItem> testResultItems)
        {
            long result = 0;

            // LINQ with grouping
            IEnumerable<IGrouping<long, TestResultItem>> linqGroup = from TestResultItem t in this.CompletedReviewsForFoodItem(foodItem, testResultItems)
                                                                     group t by t.TestResultId into groupedTestResult
                                                                     orderby groupedTestResult.Key
                                                                     select groupedTestResult;

            result = linqGroup.Count();

            return result;
        }

        /// <summary>
        /// Get a list of completed reviews for a food item.
        /// </summary>
        /// <param name="foodItem"></param>
        /// <param name="testResultItems"></param>
        /// <returns></returns>
        public List<TestResultItem> CompletedReviewsForFoodItem(FoodItem foodItem, List<TestResultItem> testResultItems)
        {
            return (from TestResultItem t in testResultItems where t.FoodItemId == foodItem.Id && t.Result > 0 select t).ToList();
        }

        /// <summary>
        /// Get a distinct list of test categories from the test results.
        /// </summary>
        /// <param name="foodItemTestResults"></param>
        /// <returns></returns>
        public List<string> CategoriesForTestResults(List<TestResultItem> foodItemTestResults)
        {
            List<string> result = new List<String>();

            IEnumerable<IGrouping<string, TestResultItem>> linqGroup = from TestResultItem t in foodItemTestResults
                                                                       group t by t.Category.Value into groupedCategories
                                                                       orderby groupedCategories.Key
                                                                       select groupedCategories;

            foreach (var item in linqGroup)
            {
                result.Add(item.Key);
            }

            return result;
        }

        /// <summary>
        /// Get the sum of testresultitem.result for the specified food item and category.
        /// </summary>
        /// <param name="foodItem"></param>
        /// <param name="category"></param>
        /// <param name="completedTests"></param>
        /// <returns></returns>
        public long ResultTotalForFoodItemAndCategory(FoodItem foodItem, string category, List<TestResultItem> completedTests)
        {
            long result = 0;

            result = (long)(from TestResultItem t in completedTests where t.FoodItemId == foodItem.Id && t.Category.Value == category select (decimal)t.Result).Sum();

            return result;
        }
    }
}
