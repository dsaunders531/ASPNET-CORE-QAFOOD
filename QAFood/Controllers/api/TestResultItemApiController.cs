using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAFood.BLL.Services;
using QAFood.BLL.ViewModels;

/// <summary>
/// Methods: Get = gets values, POST = reserved for forms, PUT = create, PATCH = update, DELETE = delete
/// </summary>

namespace QAFood.Controllers.api
{
    /// <summary>
    /// Api controller for processing test result items.
    /// </summary>
    [Authorize]
    [Route("api/TestResultItem")]
    [ApiController]
    public class TestResultItemApiController : Controller
    {
        private ReviewService ReviewService { get; set; }

        public TestResultItemApiController(ReviewService reviewService)
        {
            this.ReviewService = reviewService;
        }

        /// <summary>
        /// Get a review view model.
        /// </summary>
        /// <param name="foodParcelId"></param>
        /// <param name="selectedFoodItemId"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpGet("{foodParcelId}/{selectedFoodItemId}/{testId}")]
        public ActionResult<ReviewProcessViewModel> Get(long foodParcelId, long selectedFoodItemId, long testId)
        {
            ReviewProcessViewModel result = this.ReviewService.AssembleReviewProcessViewModel(foodParcelId, selectedFoodItemId, testId, User.Identity.Name);

            return result;
        }

        /// <summary>
        /// Update the test item.
        /// </summary>
        /// <param name="foodParcelId"></param>
        /// <param name="selectedFoodItemId"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpPatch("{foodParcelId}/{selectedFoodItemId}/{testId}")]
        public ActionResult<ReviewProcessViewModel> Patch(long foodParcelId, long selectedFoodItemId, long testId)
        {
            byte presentationValue = Convert.ToByte(Request.Query["presentationValue"]);
            byte textureValue = Convert.ToByte(Request.Query["textureValue"]);
            byte aromaValue = Convert.ToByte(Request.Query["aromaValue"]);
            byte flavourValue = Convert.ToByte(Request.Query["flavourValue"]);

            ReviewProcessViewModel result = new ReviewProcessViewModel() { FoodParcelId = foodParcelId, SelectedFoodItemId = selectedFoodItemId, TestId = testId,
                                                                            PresentationValue = presentationValue, TextureValue = textureValue, AromaValue = aromaValue, FlavourValue = flavourValue};

            this.ReviewService.SaveReview(result, User.Identity.Name);

            return result;
        }
    }


}
