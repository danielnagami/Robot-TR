using FluentValidation.Results;
using System;

namespace RobotTR.Core.Messages
{
    public class Command
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool Valid()
        {
            throw new NotImplementedException();
        }
    }
}