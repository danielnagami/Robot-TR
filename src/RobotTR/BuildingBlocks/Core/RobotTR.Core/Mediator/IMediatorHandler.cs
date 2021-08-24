using FluentValidation.Results;
using RobotTR.Core.Messages;
using System.Threading.Tasks;

namespace RobotTR.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T comamand) where T : Command;
    }
}