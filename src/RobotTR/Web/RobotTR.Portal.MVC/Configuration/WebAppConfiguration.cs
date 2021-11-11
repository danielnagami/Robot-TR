using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RobotTR.Portal.MVC.Extensions;
using RobotTR.Portal.MVC.Models;
using System.Globalization;

namespace RobotTR.Portal.MVC.Configuration
{
    public static class WebAppConfiguration
    {
        public static void AddMVCConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<AppSettings>(configuration);
        }
        public static void UseMVCConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            _ = app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}