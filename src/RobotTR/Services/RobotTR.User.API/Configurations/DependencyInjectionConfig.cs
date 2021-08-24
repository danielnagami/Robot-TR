using Microsoft.Extensions.DependencyInjection;
using RobotTR.Core.Mediator;

namespace RobotTR.User.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            //services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            //services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UsersContext>();
        }
    }
}