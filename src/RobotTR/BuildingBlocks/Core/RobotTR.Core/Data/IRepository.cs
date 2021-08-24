using RobotTR.Core.DomainObjects;
using System;

namespace RobotTR.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}