using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RobotTR.Portal.MVC.Configuration;

namespace RobotTR.Portal.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(hostingEnvironment.ContentRootPath)
           .AddJsonFile("appsettings.json", true, true)
           .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
           .AddEnvironmentVariables();

            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration();

            services.AddMVCConfiguration(Configuration);

            services.AddDependencyInjection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMVCConfiguration(env);
        }
    }
}