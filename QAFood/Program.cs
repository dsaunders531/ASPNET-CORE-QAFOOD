using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace QAFood
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Note use of .UseDefaultServiceProvider ... this needs to be setup otherwise app.ApplicationServices.GetRequiredService does not work.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();
    }
}
