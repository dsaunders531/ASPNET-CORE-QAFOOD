using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace QAFood.Controllers
{
    /// <summary>
    /// The null controller for testing layouts.
    /// </summary>
    [AllowAnonymous]
    public class NullController : Controller
    {
        /// <summary>
        /// The default page for the controller.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new ViewModelBase());
        }
    }
}
