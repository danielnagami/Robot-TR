using Microsoft.EntityFrameworkCore;
using RobotTR.Core.Data;
using RobotTR.Jobs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.Jobs.API.Data.Repository
{
    public class JobsRepository : IJobsRepository
    {
        private readonly JobsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public JobsRepository(JobsContext context)
        {
            _context = context;
        }

        public void Add(Job job)
        {
            _context.Add(job);
        }

        public bool Delete(Job job)
        {
            _context.Remove(job);

            return true;
        }

        public async Task<Job> Edit(Job job)
        {
            _context.Update(job);
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == job.Id);
        }

        public async Task<IList<Job>> GetByUser(Guid ownerId)
        {
            var query = from b in _context.Jobs
                    where b.Owner.Id.Equals(ownerId)
                    select b;

            return query.ToList();
        }

        public async Task<Job> GetById(Guid jobId)
        {
            var query = from b in _context.Jobs
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
