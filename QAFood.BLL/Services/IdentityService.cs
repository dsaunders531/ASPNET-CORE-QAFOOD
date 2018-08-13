using mezzanine.Services;
using Microsoft.AspNetCore.Identity;
using QAFood.BLL.Workers;
using QAFood.DAL.Models;

namespace QAFood.BLL.Services
{
    public sealed class IdentityService : IdentityWorker
    {
        public IdentityService(IAppConfigurationService appConfigurationService, UserManager<UserProfileModel> userManager,
                                SignInManager<UserProfileModel> signInManager, RoleManager<IdentityRole> roleManager) 
            : base(((QAFood.BLL.Services.AppConfigurationService)appConfigurationService).AppConfiguration, userManager, signInManager, roleManager)
        {
        }
    }
}
