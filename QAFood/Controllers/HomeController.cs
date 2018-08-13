using QAFood.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace QAFood.Controllers
{
    /// <summary>
    /// The default home controller.
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>
        /// The default page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new ViewModelBase());
        }

        /// <summary>
        /// The about page.
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View(new ViewModelBase());
        }

        /// <summary>
        /// The contact page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View(new ViewModelBase());
        }

        /// <summary>
        /// The error page. Note that in development mode, the developer exception page is used.
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
