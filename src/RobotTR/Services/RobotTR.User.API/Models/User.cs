//using RobotTR.Core.DomainObjects;
//using System;

//namespace RobotTR.User.API.Models
//{
//    public class User : Entity, IAggregateRoot
//    {
//        public User(Guid id, string nome, string username, string email, bool excluido, string empresa, string cargo)
//        {
//            Id = id;
//            Nome = nome;
//            Username = username;
//            Email = new Email(email);
//            Excluido = excluido;
//            Empresa = empresa;
//            Cargo = cargo;
//        }

//        protected User() { }

//        public string Nome { get; private set; }
//        public string Username { get; private set; }
//        public Email Email { get; private set; }
//        public bool Excluido { get; private set; }
//        public string Empresa { get; private set; }
//        public string Cargo { get; private set; }

//        public void ChangeEmail(string email)
//        {
//            Email = new Email(email);
//        }

//        public void ChangeRole(string role)
//        {
//            Cargo = role;
//        }
//    }
//}