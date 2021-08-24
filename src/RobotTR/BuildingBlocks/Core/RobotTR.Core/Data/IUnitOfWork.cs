using System.Threading.Tasks;

namespace RobotTR.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}