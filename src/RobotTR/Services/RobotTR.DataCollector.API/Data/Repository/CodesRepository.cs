using Microsoft.EntityFrameworkCore;
using RobotTR.Core.Data;
using RobotTR.DataCollector.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.DataCollector.API.Data.Repository
{
    public class CodesRepository
    {
        private readonly CodesContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CodesRepository(CodesContext context)
        {
            _context = context;
        }

        public void Add(Codes code)
        {
            _context.Add(code);
        }

        public bool Delete(Codes code)
        {
            _context.Remove(code);

            return true;
        }

        public async Task<Codes> Edit(Codes code)
        {
            _context.Update(code);
            return await _context.Codes.FirstOrDefaultAsync(x => x.Id == code.Id);
        }

        public async Task<IList<Codes>> GetByUser(Guid ownerId)
        {
            var query = from b in _context.Codes
                        where b..Id.Equals(ownerId)
                        select b;

            return query.ToList();
        }

        public async Task<Codes> GetById(Guid jobId)
        {
            var query = from b in _context.Codes
                        where b.Id.Equals(jobId)
                        select b;

            return query.FirstOrDefault();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
