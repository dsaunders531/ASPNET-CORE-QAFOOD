using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAFood.EF;
using QAFood.Models;
using QAFood.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace QAFood.Controllers
{
    /// <summary>
    /// The test result controller.
    /// </summary>
    [Authorize]
    public class TestResultController : Controller
    {
        private IRepository<FoodParcel> FoodParcelRepository { get; set; }

        public TestResultController(IRepository<FoodParcel> foodParcelRepository)
        {
            this.FoodParcelRepository = foodParcelRepository;
        }

        /// <summary>
        /// The default page
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admins")]
        public IActionResult Index()
        {
            List<FoodParcel> foodParcels = this.FoodParcelRepository.GetAll.ToList();
            TestResultViewModel result = new TestResultViewModel() { FoodParcels = foodParcels };
            return View(result);
        }
    }
}
