using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAFood.BLL.Models;
using QAFood.BLL.Services;
using QAFood.BLL.ViewModels;
using QAFood.DAL.Models;

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
        public IActionResult Index(int page = 1)
        {
            // Using pagination for this view.
            int itemsPerPage = 1; // the application default does not apply as there will be few food parcels to review.

            // Get the paginated data
            FoodParcelsViewModel foodParcelsViewModel = new FoodParcelsViewModel()
            {
                FoodParcels = this.ReviewService.GetFoodParcelsPaginated((page -1) * itemsPerPage, itemsPerPage),     
                Pagination = new PaginationModel()
                            {
                                CurrentPage = page,
                                ItemsPerPage = itemsPerPage,
                                ItemCount = this.ReviewService.GetFoodParcels().Count,
                                PageAction = "Index"
                            }
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
