using RobotTR.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.User.API.Models
{
    public interface IUsersRepository : IRepository<User>
    {
        void Add(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserByUsername(string username);
    }
}