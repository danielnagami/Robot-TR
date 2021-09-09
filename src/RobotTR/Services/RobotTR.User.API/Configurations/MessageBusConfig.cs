using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RobotTR.Core.Utils;
using RobotTR.MessageBus;
using RobotTR.User.API.Services;

namespace RobotTR.User.API.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisterUserIntegrationHandler>();
        }
    }
}