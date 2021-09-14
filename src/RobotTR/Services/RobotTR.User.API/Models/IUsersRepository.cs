using RobotTR.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.User.API.Models
{
    public interface IUsersRepository : IRepository<RobotTR.Core.Models.User>
    {
        void Add(RobotTR.Core.Models.User user);
        Task<IEnumerable<RobotTR.Core.Models.User>> GetAll();
        Task<RobotTR.Core.Models.User> GetUserByUsername(string username);
    }
}