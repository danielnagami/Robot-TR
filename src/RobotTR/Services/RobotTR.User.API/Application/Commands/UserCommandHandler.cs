using FluentValidation.Results;
using MediatR;
using RobotTR.Core.Messages;
using RobotTR.User.API.Application.Events;
using RobotTR.User.API.Commands;
using RobotTR.User.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RobotTR.User.API.Application.Commands
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<RegisterUserCommand, ValidationResult>
    {
        private readonly IUsersRepository _clienteRepository;

        public UserCommandHandler(IUsersRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegisterUserCommand message, CancellationToken cancellationToken)
        {
            if (!message.Valid()) return message.ValidationResult;

            var cliente = new RobotTR.Core.Models.User(message.Id, message.Nome, message.Username, message.Email, false, message.Empresa, message.Cargo);

            var clienteExitente = await _clienteRepository.GetUserByUsername(cliente.Username);

            if (clienteExitente != null)
            {
                AddError("Esse username já existe.");
                return ValidationResult;
            }

            _clienteRepository.Add(cliente);

            cliente.AddEvent(new UserRegistredEvent(message.Id, message.Nome, message.Username, message.Email, message.Empresa, message.Cargo));

            return await PersistData(_clienteRepository.UnitOfWork);
        }
    }
}