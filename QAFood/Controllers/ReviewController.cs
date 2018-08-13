using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAFood.BLL.Workers;
using QAFood.DAL;
using QAFood.DAL.Models;
using QAFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QAFood.BLL.ViewModels;
using QAFood.BLL.Services;

namespace QAFood.Controllers
{
    /// <summary>
    /// The controller for reviews.
    /// </summary>
    [Authorize]
    public class ReviewController : Controller
    {
        private ReviewService ReviewService { get; set; }

        public ReviewController(ReviewService reviewService)
        {
            this.ReviewService = reviewService;
        }

        /// <summary>
        /// Get a list of all the food parcels which need a review.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            FoodParcelsViewModel foodParcelsViewModel = new FoodParcelsViewModel()
            {
                FoodParcels = this.ReviewService.GetFoodParcels()
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

            FoodParcel foodParcel = this.ReviewService.GetFoodParcel(parcelId);

            if (foodParcel != null)
            {
                TestResult test = this.ReviewService.GetTestResult(parcelId, User.Identity.Name);

                if (test == null)
                {
                    test = this.ReviewService.CreateTest(foodParcel, User.Identity.Name);
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
            if (ModelState.IsValid == true)
            {
                ReviewProcessViewModel result = this.ReviewService.AssembleReviewProcessViewModel(model, User.Identity.Name);
               
                return View(result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "There is something wrong with your selection");
                return RedirectToAction("ReviewStart", "Review");
            }
        }
    }
}
