using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RobotTR.Core.Mediator;
using RobotTR.Jobs.API.Data;
using RobotTR.Jobs.API.Data.Repository;
using RobotTR.Jobs.API.Models;
using RobotTR.WebAPI.Core.User;

namespace RobotTR.Jobs.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddScoped<JobsContext>();
        }
    }
}