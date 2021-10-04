using Microsoft.Extensions.DependencyInjection;
using RobotTR.Core.Mediator;
using RobotTR.Jobs.API.Data;
using RobotTR.Jobs.API.Data.Repository;
using RobotTR.Jobs.API.Models;

namespace RobotTR.Jobs.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddScoped<JobsContext>();
        }
    }
}