using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace QAFood.Controllers
{
    /// <summary>
    /// The legal controller.
    /// </summary>
    [AllowAnonymous] 
    public class LegalController : Controller
    {
        /// <summary>
        /// The default page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new ViewModelBase());
        }

        /// <summary>
        /// Cookie page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Cookies()
        {
            return View(new ViewModelBase());
        }

        /// <summary>
        /// The GDPR - data protection page.
        /// </summary>
        /// <returns></returns>
        public IActionResult GDPR()
        {
            return View(new ViewModelBase());
        }
    }
}
