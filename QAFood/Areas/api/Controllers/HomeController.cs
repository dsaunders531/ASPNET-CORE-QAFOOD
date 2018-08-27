using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QAFood.Areas.api.Models;
using QAFood.Areas.api.Infrastructure;

namespace QAFood.Areas.api.Controllers
{
    [Area("api")]
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// The default page in the api area.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ApiDiscoveryManager discoveryManager = new ApiDiscoveryManager(System.Reflection.Assembly.GetExecutingAssembly());

            ApiControllersViewModel result = discoveryManager.Discover();

            return View(result);
        }
    }
}
