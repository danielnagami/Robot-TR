using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RobotTR.DataCollector.API.Configuration
{
    public static class APIConfig
    {
        public static void AddAPIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}