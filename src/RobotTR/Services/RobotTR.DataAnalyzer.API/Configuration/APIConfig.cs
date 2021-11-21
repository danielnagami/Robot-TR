using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RobotTR.DataAnalyzer.API.Configuration
{
    public static class APIConfig
    {
        public static void AddAPIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
        }
    }
}