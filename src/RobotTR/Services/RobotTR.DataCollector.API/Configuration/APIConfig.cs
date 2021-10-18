using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RobotTR.Core.Mediator;
using RobotTR.DataCollector.API.Data;
using RobotTR.DataCollector.API.Data.Repository;
using RobotTR.DataCollector.API.Models;

namespace RobotTR.DataCollector.API.Configuration
{
    public static class APIConfig
    {
        public static void AddAPIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CodesContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddControllers();

            services.AddMediatR(typeof(Startup));

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<ICodesRepository, CodesRepository>();

            services.AddScoped<CodesContext>();
        }
    }
}