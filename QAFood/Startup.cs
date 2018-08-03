using mezzanine.Extensions;
using mezzanine.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QAFood.EF;
using QAFood.Models;

namespace QAFood
{
    /// <summary>
    /// The startup class where services and defaults are setup.
    /// </summary>
    public class Startup
    {
        private IHostingEnvironment Environment { get; set; } = null;
        private QAFood.Services.AppConfigurationService ConfigurationService { get; set; }
        private AppConfigurationModel AppConfiguration { get => this.ConfigurationService.AppConfiguration; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
            this.ConfigurationService = new QAFood.Services.AppConfigurationService(environment);
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Built-in services
            services.AddMemoryCache();
            services.AddSession();
            services.AddLocalization();
            services.AddAntiforgery();

            // AspNetCore Identity
            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(this.AppConfiguration.ConnectionStrings.Authentication)); // The user database
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
            // End Identity

            // Application Data
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(this.AppConfiguration.ConnectionStrings.Content)); // Application data
            services.AddTransient<IRepository<FoodParcel>, FoodParcelRepository>();
            services.AddTransient<IRepository<FoodItem>, FoodItemRespository>();
            services.AddTransient<IRepository<TestResult>, TestResultRepository>();
            services.AddTransient<IRepository<TestResultItem>, TestResultItemsRepository>();
            services.AddTransient<IRepository<LOV>, ListOfValuesRepository>();
            // End application data

            // Custom services
            services.AddSingleton<IAppConfigurationService>(this.ConfigurationService); // provides strongly typed access to appsettings.json.

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Create and update the database automatically (like doing Update-Database)
            // https://stackoverflow.com/questions/42355481/auto-create-database-in-entity-framework-core
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                AuthenticationDbContext authenticationDbContext = serviceScope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
                authenticationDbContext.Database.Migrate();

                ApplicationDbContext applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                applicationDbContext.Database.Migrate();
            }

            // Seed data
            AuthenticationDbContext.CreateDefaultRoles(app.ApplicationServices, this.AppConfiguration).Wait();
            AuthenticationDbContext.CreateAdminAccount(app.ApplicationServices, this.AppConfiguration).Wait();

            // Configure the custom logger.
            if (this.AppConfiguration.Logging.StdOutEnabled == true)
            {
                loggerFactory.AddConsole(this.AppConfiguration.Logging.StdOutLevel.ToLogLevel());
            }

            if (this.AppConfiguration.Logging.LogXMLEnabled == true)
            {
                loggerFactory.AddProvider(new XMLLoggerProvider(this.AppConfiguration.Logging.LogXMLLevel.ToLogLevel(),
                                                                this.ConfigurationService.WebRootPath + this.AppConfiguration.Logging.LogXMLPath,
                                                                this.AppConfiguration.Logging.LogRotateMaxEntries));
            }

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseSession();

            // AspNetCore Identity
            app.UseAuthentication();
            // End identity

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.EnsurePopulated(app.ApplicationServices);
        }
    }
}
