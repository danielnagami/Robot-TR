using RobotTR.Core.Data;
using RobotTR.User.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.User.API.Data.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Models.User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Models.User> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}