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

        public async Task Add(Job job)
        {
            await _context.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid jobId)
        {
            var job = await GetById(jobId);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Job> Edit(Job job)
        {
            _context.Update(job);
            //await _context.Commit();
            await _context.SaveChangesAsync();
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == job.Id);
        }

        public async Task<IList<Job>> GetByUser(Guid ownerId)
        {
            return _context.Jobs.Where(j => j.OwnerId == ownerId).ToList();
        }

        public async Task<Job> GetById(Guid jobId)
        {
            return await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
