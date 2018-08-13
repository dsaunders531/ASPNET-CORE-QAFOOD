using Microsoft.AspNetCore.Identity;
using QAFood.BLL.ViewModels.Authentication;
using QAFood.DAL.Models;
using System.Threading.Tasks;

namespace QAFood.BLL.Workers
{
    /// <summary>
    /// Identity worker
    /// </summary>
    public class IdentityWorker
    {
        private UserManager<UserProfileModel> UserManager { get; set; }
        private SignInManager<UserProfileModel> SignInManager { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private string DefaultRole { get; set; }

        public IdentityWorker(AppConfigurationModel appConfiguration, UserManager<UserProfileModel> userManager, 
                                SignInManager<UserProfileModel> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.RoleManager = roleManager;
            this.DefaultRole = appConfiguration.SeedData.DefaultRole;
        }

        public async Task<IdentityResult> CreateAccountAsync(CreateAccountViewModel model)
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
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            SignInResult result = null;

            UserProfileModel user = await this.UserManager.FindByEmailAsync(model.Email);
            
            if (user != null)
            {
                await this.SignInManager.SignOutAsync();

                result = await this.SignInManager.PasswordSignInAsync(user, model.Password, false, false);
            }
            else
            {
                result = null;
            }

            return result;
        }

        public async Task<bool> Logout()
        {
            bool result = true;

            await SignInManager.SignOutAsync();

            return result;
        }
    }
}
