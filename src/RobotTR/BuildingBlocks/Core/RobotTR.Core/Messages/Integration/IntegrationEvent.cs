using System;

namespace RobotTR.Core.Messages.Integration
{
    public class IntegrationEvent : Event { }
    public class UserRegisterIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public UserRegisterIntegrationEvent(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            CPF = cpf;
        }
    }
}