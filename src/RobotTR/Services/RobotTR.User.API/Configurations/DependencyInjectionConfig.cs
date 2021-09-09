using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RobotTR.Core.Mediator;
using RobotTR.User.API.Application.Commands;
using RobotTR.User.API.Application.Events;
using RobotTR.User.API.Commands;
using RobotTR.User.API.Data;
using RobotTR.User.API.Models;

namespace RobotTR.User.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterUserCommand, ValidationResult>, UserCommandHandler>();

            services.AddScoped<INotificationHandler<UserRegistredEvent>, UserEventHandler>();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<UsersContext>();
        }
    }
}