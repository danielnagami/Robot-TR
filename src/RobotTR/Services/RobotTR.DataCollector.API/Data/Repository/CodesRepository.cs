using Microsoft.EntityFrameworkCore;
using RobotTR.Core.Data;
using RobotTR.DataCollector.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.DataCollector.API.Data.Repository
{
    public class CodesRepository : ICodesRepository
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

        public IList<Codes> GetByProjectId(Guid projectId)
        {
            var query = _context.Codes.Where(x => x.Id.Equals(projectId));

            return query.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void DropProject(Guid projectId)
        {
            var classes = _context.Codes.Where(c => c.Id.Equals(projectId));

            foreach (var item in classes)
            {
                _context.Codes.Remove(item);
            }
        }
    }
}