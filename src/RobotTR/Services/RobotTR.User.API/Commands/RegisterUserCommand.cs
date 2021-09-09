using FluentValidation;
using RobotTR.Core.DomainObjects;
using RobotTR.Core.Messages;
using System;

namespace RobotTR.User.API.Commands
{
    public class RegisterUserCommand : Command
    {
        public RegisterUserCommand(Guid id, string nome, string username, string email, string empresa, string cargo)
        {
            AggregateId = id;
            Nome = nome;
            Username = username;
            Email = email;
            Empresa = empresa;
            Cargo = cargo;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Empresa { get; private set; }
        public string Cargo { get; private set; }

        public override bool Valid()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarClienteValidation : AbstractValidator<RegisterUserCommand>
        {
            public RegistrarClienteValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido");

                RuleFor(c => c.Nome)
                   .NotEmpty()
                   .WithMessage("O nome do cliente não foi informado.");

                RuleFor(c => c.Email)
                    .Must(HasValidEMail)
                    .WithMessage("O e-mail informado não é válido.");

                RuleFor(c => c.Username)
                   .NotEmpty()
                   .WithMessage("O username não foi informado.");


                RuleFor(c => c.Cargo)
                   .NotEmpty()
                   .WithMessage("O cargo não foi informado.");


                RuleFor(c => c.Empresa)
                   .NotEmpty()
                   .WithMessage("A empresa não foi informada.");
            }

            protected static bool HasValidEMail(string email)
            {
                return Core.DomainObjects.Email.Validate(email);
            }
        }
    }
}