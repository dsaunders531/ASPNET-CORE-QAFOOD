using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAFood.DAL;
using QAFood.DAL.Models;
using QAFood.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QAFood.BLL.ViewModels;
//using QAFood.BLL.Workers;
using QAFood.BLL.Services;

namespace QAFood.Controllers
{
    /// <summary>
    /// The test result controller.
    /// </summary>
    [Authorize]
    public class TestResultController : Controller
    {
        private TestResultService TestResultService { get; set; }

        public TestResultController(TestResultService testResultService)
        {
            this.TestResultService = testResultService;
        }

        /// <summary>
        /// The default page
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admins")]
        public IActionResult Index()
        {            
            TestResultViewModel result = new TestResultViewModel() { FoodParcels = this.TestResultService.GetFoodParcels() };
            return View(result);
        }
    }
}
