using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAFood.EF;
using QAFood.Models;
using QAFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QAFood.Controllers
{
    /// <summary>
    /// The controller for reviews.
    /// </summary>
    [Authorize]
    public class ReviewController : Controller
    {
        private IRepository<FoodParcel> FoodParcelRepository { get; set; }
        private IRepository<FoodItem> FoodItemRepository { get; set; }
        private IRepository<TestResult> TestResultRepository { get; set; }
        private IRepository<TestResultItem> TestResultItemRepository { get; set; }
        private IRepository<LOV> ListOfValuesRepository { get; set; }

        public ReviewController(IRepository<FoodParcel> foodParcelRepository, 
                                IRepository<FoodItem> foodItemRepository,
                                IRepository<TestResult> testResultRepository,
                                IRepository<TestResultItem> testResultItemRepository,
                                IRepository<LOV> listOfValuesRepository)
        {
            this.FoodParcelRepository = foodParcelRepository;
            this.FoodItemRepository = foodItemRepository;
            this.TestResultRepository = testResultRepository;
            this.TestResultItemRepository = testResultItemRepository;
            this.ListOfValuesRepository = listOfValuesRepository;
        }

        /// <summary>
        /// Get a list of all the food parcels which need a review.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            FoodParcelsViewModel foodParcelsViewModel = new FoodParcelsViewModel()
            {
                FoodParcels = this.FoodParcelRepository.GetAll.OrderBy(o => o.PostedDate).ToList()
            };

            return View(foodParcelsViewModel);
        }

        /// <summary>
        /// Start a review.
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public IActionResult ReviewStart(long parcelId)
        {
            // Create the review for the user if it does not exist
            string username = User.Identity.Name;

            FoodParcel foodParcel = this.FoodParcelRepository.Get(parcelId);

            // REVIEW - this controller action is doing too much work. All this code should be moved to a manager class.
            if (foodParcel != null)
            {
                TestResult test = this.TestResultRepository.GetByUser(parcelId, User.Identity.Name);

                if (test == null)
                {
                    List<LOV> measurementCategories = (from LOV l in this.ListOfValuesRepository.GetAll
                                                       where l.Context == "TestCategory"
                                                       select l).OrderBy(o => o.Value).ToList();

                    List<FoodItem> selectedFoodItems = foodParcel.FoodItems;

                    test = new TestResult()
                    {
                        FoodParcelId = parcelId,
                        TestDate = DateTime.Now,
                        Name = foodParcel.Name + " Review",
                        UserName = username
                    };

                    this.TestResultRepository.Create(test);
                    this.TestResultRepository.Save();

                    // Add the food items to the test
                    foreach (FoodItem item in selectedFoodItems)
                    {
                        foreach (LOV category in measurementCategories)
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

                    test = this.TestResultRepository.GetByUser(parcelId, User.Identity.Name); // Check the new item has been saved properly
                }

                ReviewStartViewModel review = new ReviewStartViewModel() { TestId = test.Id, TestName = test.Name, FoodParcel = foodParcel };

                return View(review);
            }
            else
            {
                return RedirectToAction("Index", "Review");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReviewProcess(ReviewStartResultViewModel model)
        {
            // REVIEW - this controller action is doing too much work. All this code should be moved to a manager class.
            if (ModelState.IsValid == true)
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
                                                         && t.TestResult.UserName == User.Identity.Name
                                                        select t).ToList();

                // The disadvantage of using hard-coded values is that it is difficult to change and add new things later 
                // without changing code, recompiling, testing and deploying.
                result.PresentationValue = (from TestResultItem t in testResultItems where t.Category.Value == "Presentation" select t.Result).FirstOrDefault();
                result.TextureValue = (from TestResultItem t in testResultItems where t.Category.Value == "Texture" select t.Result).FirstOrDefault();
                result.AromaValue = (from TestResultItem t in testResultItems where t.Category.Value == "Aroma" select t.Result).FirstOrDefault();
                result.FlavourValue = (from TestResultItem t in testResultItems where t.Category.Value == "Flavour" select t.Result).FirstOrDefault();

                return View(result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "There is something wrong with your selection");
                return RedirectToAction("ReviewStart", "Review");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult SaveReview(ReviewProcessViewModel model)
        {
            // REVIEW - this controller action is doing too much work. All this code should be moved to a manager class.

            if (ModelState.IsValid == true)
            {
                // Save Review data.
                // HACK - I need to do this call otherwise the linq to get TestResult is null. Test result (t.TestResult) does not work automatically!
                TestResult testResult = this.TestResultRepository.Get(model.TestId);
                model.TestName = testResult.Name;

                // Match the values in the model with database
                List<TestResultItem> testResultItems = (from TestResultItem t in this.TestResultItemRepository.GetAll.AsParallel().WithDegreeOfParallelism(2)
                                                        where t.FoodItemId == model.SelectedFoodItemId
                                                         && t.TestResultId == model.TestId
                                                         && t.TestResult.UserName == User.Identity.Name
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

                // Review another item?
                return RedirectToAction("ReviewComplete", "Review");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "There is something wrong with your selection");
                return RedirectToAction("ReviewStart", "Review");
            }
        }

        public IActionResult ReviewComplete()
        {
            // All done - show a thanks message to user and redirect them to review again.
            return View(new ViewModelBase());
        }
    }
}
