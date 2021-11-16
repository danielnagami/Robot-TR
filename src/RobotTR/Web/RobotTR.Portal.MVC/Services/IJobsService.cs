using RobotTR.Portal.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public interface IJobsService
    {
        Task<IEnumerable<JobViewModel>> GetJobs(Guid ownerId);
        IList<LanguagesEnum> GetLanguages();
        IList<FrameworksEnum> GetFrameworks();
        Task Create(JobViewModel job);
        Task Delete(Guid jobId);
        Task Edit(JobViewModel job);
        Task<JobViewModel> Read(Guid jobId);
    }
}