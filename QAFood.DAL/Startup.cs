using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QAFood.DAL.Models;

namespace QAFood.DAL
{
    /// <summary>
    /// Public static methods to setup services and configure the application and identity database
    /// </summary>
    public static class Startup
    {
        public static void ConfigureServices(AppConfigurationModel appConfiguration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(appConfiguration.ConnectionStrings.Content)); // Application data
            services.AddTransient<IRepository<FoodParcel>, FoodParcelRepository>();
            services.AddTransient<IRepository<FoodItem>, FoodItemRespository>();
            services.AddTransient<IRepository<TestResult>, TestResultRepository>();
            services.AddTransient<IRepository<TestResultItem>, TestResultItemsRepository>();
            services.AddTransient<IRepository<TestItemCategory>, TestItemCategoriesRepository>();
        }

        public static void Configure(IApplicationBuilder app)
        {
            // Create and update the database automatically (like doing Update-Database)
            // https://stackoverflow.com/questions/42355481/auto-create-database-in-entity-framework-core
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {                
                ApplicationDbContext applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                applicationDbContext.Database.Migrate();
            }

            SeedData.EnsurePopulated(app.ApplicationServices);
        }

        public static void ConfigureIdentityServices(AppConfigurationModel appConfiguration, IServiceCollection services)
        {
            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(appConfiguration.ConnectionStrings.Authentication)); // The user database
            
            // Setup the password validation requirements eg: Password1!
            services.AddIdentity<UserProfileModel, IdentityRole>(
                    opts => {
                        opts.User.RequireUniqueEmail = true;

                        opts.Password.RequiredLength = 9;
                        opts.Password.RequireNonAlphanumeric = false;
                        opts.Password.RequireLowercase = true;
                        opts.Password.RequireUppercase = true;
                        opts.Password.RequireDigit = true;
                    }
                ).AddEntityFrameworkStores<AuthenticationDbContext>(); // The model
        }

        public static void ConfigureIdentity(AppConfigurationModel appConfiguration, IApplicationBuilder app)
        {
            // Create and update the database automatically (like doing Update-Database)
            // https://stackoverflow.com/questions/42355481/auto-create-database-in-entity-framework-core
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                AuthenticationDbContext authenticationDbContext = serviceScope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
                authenticationDbContext.Database.Migrate();
            }

            // Seed data
            AuthenticationDbContext.CreateDefaultRoles(app.ApplicationServices, appConfiguration).Wait();
            AuthenticationDbContext.CreateAdminAccount(app.ApplicationServices, appConfiguration).Wait();

            app.UseAuthentication();
        }
    }
}
