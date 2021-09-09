using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RobotTR.User.API.Application.Events
{
    public class UserEventHandler : INotificationHandler<UserRegistredEvent>
    {
        public Task Handle(UserRegistredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}