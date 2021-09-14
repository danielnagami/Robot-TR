using Microsoft.AspNetCore.Mvc;
using RobotTR.Jobs.API.Models;
using RobotTR.WebAPI.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace RobotTR.Jobs.API.Controllers
{
    [Route("api/Jobs")]
    public class JobsController : MainController
    {
        private IJobsRepository _jobsRepository;
        public JobsController(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Job job)
        {
            _jobsRepository.Add(job);

            return CustomResponse();
        }

        [HttpPost("ReadAllByUser")]
        public async Task<IActionResult> ReadAllByUser(Guid ownerId)
        {
            var jobs = await _jobsRepository.GetByUser(ownerId);
            return CustomResponse(jobs);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(Job job)
        {
            var newJob = await _jobsRepository.Edit(job);
            return CustomResponse(newJob);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid jobId)
        {
            var job = _jobsRepository.GetById(jobId);
            var response = _jobsRepository.Delete(job.Result);
            return CustomResponse(response);
        }
    }
}