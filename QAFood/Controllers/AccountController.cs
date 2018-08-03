using mezzanine.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QAFood.Models;
using QAFood.ViewModels.Authentication;
using System.Threading.Tasks;

namespace QAFood.Controllers
{
    /// <summary>
    /// Controller for self-service Identity
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<UserProfileModel> UserManager { get; set; }
        private SignInManager<UserProfileModel> SignInManager { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private string DefaultRole { get; set; }

        public AccountController(UserManager<UserProfileModel> userManager, SignInManager<UserProfileModel> signInManager, RoleManager<IdentityRole> roleManager, IAppConfigurationService appConfigurationService)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.RoleManager = roleManager;
            this.DefaultRole = ((QAFood.Services.AppConfigurationService)appConfigurationService).AppConfiguration.SeedData.DefaultRole;
        }

        /// <summary>
        /// Create a user account.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ViewResult CreateAccount() => View(new CreateAccountViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel model)
        {
            if (ModelState.IsValid == true)
            {                
                UserProfileModel user = new UserProfileModel { UserName = model.Name, Email = model.Email };
                IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded == true)
                {
                    // Setup the role for the user if it does not exist.
                    if (await RoleManager.FindByNameAsync(this.DefaultRole) == null)
                    {
                        await RoleManager.CreateAsync(new IdentityRole(this.DefaultRole));
                    }
                    await UserManager.AddToRoleAsync(user, this.DefaultRole);

                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await this.SignInManager.PasswordSignInAsync(user, model.Password, false, false);
                    
                    return RedirectToAction("Index", "Home", null); // return the user to the home page since we don't have a return to url in the model.
                }
                else
                {
                    // Oh dear something went wrong.
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Login to your account.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel() { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                UserProfileModel user = await this.UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await this.SignInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await this.SignInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (signInResult.Succeeded == true)
                    {
                        return Redirect(model.ReturnUrl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "We could not sign you in. Please check your password.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "We cannot find you. Please create an account.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Logout from your account.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
