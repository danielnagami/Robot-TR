using System;

namespace RobotTR.Core.Messages.Integration
{
    public class IntegrationEvent : Event { }
    public class UserRegisterIntegrationEvent : IntegrationEvent
    {
        public UserRegisterIntegrationEvent(Guid id, string nome, string username, string email, string empresa, string cargo)
        {
            Id = id;
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
    }
}