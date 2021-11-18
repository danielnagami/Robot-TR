using RobotTR.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.Jobs.API.Models
{
    public interface IJobsRepository : IRepository<Job>
    {
        Task Add(Job job);
        Task<IList<Job>> GetByUser(Guid ownerId);
        Task<Job> GetById(Guid jobId);
        Task<Job> Edit(Job job);
        Task<bool> Delete(Guid jobId);
    }
}