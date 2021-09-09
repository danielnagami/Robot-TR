using RobotTR.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.User.API.Application.Events
{
    public class UserRegistredEvent : Event
    {
        public UserRegistredEvent(Guid id, string nome, string username, string email, string empresa, string cargo)
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
    }
}