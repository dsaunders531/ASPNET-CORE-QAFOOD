using mezzanine.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAFood.Areas.api.Models;
using QAFood.BLL.Services;
using QAFood.BLL.ViewModels;
using System;

/// <summary>
/// Api Methods: Get = gets values, POST = gets values with parameters, PUT = create, PATCH = update, DELETE = delete
/// </summary>
namespace QAFood.Areas.api.Controllers
{
    /// <summary>
    /// Api controller for processing test result items.
    /// </summary>
    [Authorize]
    [Area("api")]
    [ApiController]
    [Route("api/TestResultItem")]   
    public class TestResultItemApiController : Controller
    {
        private ReviewService ReviewService { get; set; }
        private IAppConfigurationService AppConfigurationService { get; set; }
        /// <summary>
        /// Create an instance of the TestResultItemApiController.
        /// </summary>
        /// <param name="appConfigurationService"></param>
        /// <param name="reviewService"></param>
        public TestResultItemApiController(IAppConfigurationService appConfigurationService, ReviewService reviewService)
        {
            this.ReviewService = reviewService;
            this.AppConfigurationService = appConfigurationService;
        }

        /// <summary>
        /// Get a review data line.
        /// </summary>
        /// <param name="model">A ReviewStartApiO object.</param>
        /// <returns>A ReviewProcessingApiO</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost()]
        public ActionResult<ReviewProcessApiO> Post([FromBody] ReviewStartApiO model)
        {
            if (this.AppConfigurationService.IsDevelopment == true)
            {
                System.Threading.Thread.Sleep(2000); // sleep for 2 seconds just to see the animations working. This should show a cursor only.
            }

            try
            {
                if (ModelState.IsValid == false)
                {
                    ModelState.TryAddModelError(string.Empty, "The data is not valid");
                    return BadRequest(ModelState);
                }
                else
                {
                    ReviewProcessViewModel businessResult = this.ReviewService.AssembleReviewProcessViewModel(model.FoodParcelId, model.SelectedFoodItemId, model.TestId, User.Identity.Name);

                    if (businessResult != null)
                    {
                        using (Transposition transposition = new Transposition())
                        {
                            ReviewProcessApiO result = transposition.Transpose<ReviewProcessViewModel, ReviewProcessApiO>(businessResult, new ReviewProcessApiO());
                            return result;
                        }

                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                // return internal server error.
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Update a review data line.
        /// </summary>
        /// <param name="model">A ReviewProcessApiO to update.</param>
        /// <returns>Sucess code OK 200 if the item was saved.</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPatch()]
        public ActionResult<ReviewProcessApiO> Patch([FromBody] ReviewProcessApiO model)
        {
            if (this.AppConfigurationService.IsDevelopment == true)
            {
                System.Threading.Thread.Sleep(7000); // sleep for 7 seconds just to see the animations working.
            }
            
            try
            {
                if (ModelState.IsValid == false)
                {
                    ModelState.TryAddModelError(string.Empty, "The data is not valid");
                    return BadRequest(ModelState);
                }
                else
                {
                    using (Transposition transposition = new Transposition())
                    {
                        ReviewProcessViewModel result = transposition.Transpose<ReviewProcessApiO, ReviewProcessViewModel>(model, new ReviewProcessViewModel());

                        this.ReviewService.SaveReview(result, User.Identity.Name);

                        return model; // return the same object as was sent with status code 200 ok.
                    }
                }
            }
            catch (Exception e)
            {
                // return internal server error.
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Just an example to test the api explorer. It has query parameters and a JSON body.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="text"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/Example/{number}")]
        [ProducesResponseType(200)]
        public ActionResult<ReviewProcessApiO> Example([FromBody] ReviewStartApiO model, [FromRoute] int number, [FromQuery] long othernumber, [FromQuery] string text = "test example")
        {
            return this.Post(model);
        }
    }
}
