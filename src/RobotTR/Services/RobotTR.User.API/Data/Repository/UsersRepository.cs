using Microsoft.EntityFrameworkCore;
using RobotTR.Core.Data;
using RobotTR.User.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.User.API.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UsersRepository(UsersContext context)
        {
            _context = context;
        }

        public void Add(RobotTR.Core.Models.User user)
        {
            _context.Add(user);
        }

        public async Task<IEnumerable<RobotTR.Core.Models.User>> GetAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<RobotTR.Core.Models.User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Username == username);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}