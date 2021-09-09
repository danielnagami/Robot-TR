using NetDevPack.Domain;
using System.Text.RegularExpressions;

namespace RobotTR.Core.DomainObjects
{
    public class Email
    {
        public const int EmailMaxLength = 254;
        public const int EmailMinLength = 5;
        public string Address { get; private set; }

        protected Email() { }

        public Email(string address)
        {
            if (!Validate(address)) throw new DomainException("Invalid e-mail");
            Address = address;
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return regexEmail.IsMatch(email);
        }
    }
}