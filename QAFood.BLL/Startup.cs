using mezzanine.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using QAFood.BLL.Services;
using QAFood.DAL.Models;

namespace QAFood.BLL
{
    /// <summary>
    /// Startup for the business logic layer. This also sets up entity framework because BLL depends on it.
    /// </summary>
    public static class Startup
    {
        public static void ConfigureServices(AppConfigurationModel appConfiguration, IServiceCollection services, IHostingEnvironment environment)
        {
            // Custom services
            services.AddSingleton<IAppConfigurationService>(new QAFood.BLL.Services.AppConfigurationService(environment)); // provides strongly typed access to appsettings.json.

            // Application Data - this is a dependant of business logic so we start it here
            QAFood.DAL.Startup.ConfigureServices(appConfiguration, services);
            
            // AspNetCore Identity
            QAFood.DAL.Startup.ConfigureIdentityServices(appConfiguration, services);

            services.AddTransient<IdentityService, IdentityService>();
            services.AddTransient<ReviewService, ReviewService>();
            services.AddTransient<TestResultService, TestResultService>();            
        }

        public static void Configure(AppConfigurationModel appConfiguration, IApplicationBuilder app)
        {
            // Application Data
            QAFood.DAL.Startup.Configure(app);
            
            // AspNetCore Identity
            QAFood.DAL.Startup.ConfigureIdentity(appConfiguration, app);
        }
    }
}
