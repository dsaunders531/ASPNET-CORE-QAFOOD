using mezzanine.Extensions;
using mezzanine.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QAFood.DAL.Models;

namespace QAFood
{
    /// <summary>
    /// The startup class where services and defaults are setup.
    /// </summary>
    public class Startup
    {
        private IHostingEnvironment Environment { get; set; } = null;
        private QAFood.BLL.Services.AppConfigurationService ConfigurationService { get; set; }
        private AppConfigurationModel AppConfiguration { get => this.ConfigurationService.AppConfiguration; }

        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
            this.ConfigurationService = new QAFood.BLL.Services.AppConfigurationService(environment);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Built-in services
            services.AddMemoryCache();
            services.AddSession();
            services.AddLocalization();
            services.AddAntiforgery();
            
            // Business logic services - this also configures the database services & Identity services
            QAFood.BLL.Startup.ConfigureServices(this.AppConfiguration, services, this.Environment);

            services.AddMvc(); //.AddMvcOptions(opts => { opts.RespectBrowserAcceptHeader = true; opts.ReturnHttpNotAcceptable = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configure the custom logger.
            this.ConfigureLogger(loggerFactory);

            // Business Logic Configuration - this also configures database services & Identity services
            QAFood.BLL.Startup.Configure(this.AppConfiguration, app);            

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

            // Note: using a seperate area for the api
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areas", template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #region "Logger"
        private void ConfigureLogger(ILoggerFactory loggerFactory)
        {
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
        }
        #endregion
    }
}
