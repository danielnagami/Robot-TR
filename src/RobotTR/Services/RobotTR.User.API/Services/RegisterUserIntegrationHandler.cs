using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RobotTR.Core.Mediator;
using RobotTR.Core.Messages.Integration;
using RobotTR.MessageBus;
using RobotTR.User.API.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RobotTR.User.API.Services
{
    public class RegisterUserIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterUserIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetRespond()
        {
            _bus.RespondAsync<UserRegisterIntegrationEvent, ResponseMessage>(async request =>
              await RegisterUser(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetRespond();

            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetRespond();
        }

        public async Task<ResponseMessage> RegisterUser(UserRegisterIntegrationEvent message)
        {
            var clienteCommand = new RegisterUserCommand(message.Id, message.Nome, message.Username, message.Email, message.Empresa, message.Cargo);

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
