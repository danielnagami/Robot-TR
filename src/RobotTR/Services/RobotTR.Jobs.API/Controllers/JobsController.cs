using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotTR.Jobs.API.Models;
using RobotTR.WebAPI.Core.Controllers;
using RobotTR.WebAPI.Core.User;

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RobotTR.Jobs.API.Controllers
{
    [Authorize, Route("api/Jobs")]
    public class JobsController : MainController
    {
        private IJobsRepository _jobsRepository;
        private IAspNetUser _aspNetUser;
        public JobsController(IJobsRepository jobsRepository, IAspNetUser aspNetUser)
        {
            _jobsRepository = jobsRepository;
            _aspNetUser = aspNetUser;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(JobCreation job)
        {
            await _jobsRepository.Add(new Job() 
            {
                Id = Guid.NewGuid(),
                Title = job.Title,
                Level = job.Level,
                Languages = job.Languages,
                Frameworks = job.Frameworks,
                OwnerId = _aspNetUser.GetUserId()
            });

            return CustomResponse();
        }

        [HttpGet("ReadAllByUser")]
        public async Task<IActionResult> ReadAllByUser()
        {
            var jobs = await _jobsRepository.GetByUser(_aspNetUser.GetUserId());
            return CustomResponse(jobs);
        }

        [HttpGet("Read")]
        public async Task<IActionResult> Read(Guid jobId)
        {
            var jobs = await _jobsRepository.GetById(jobId);
            return CustomResponse(jobs);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Job job)
{
            var jobs = await _jobsRepository.Edit(job);
            return CustomResponse(jobs);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid jobId)
        {
            var jobs = await _jobsRepository.Delete(jobId);
            return CustomResponse(jobs);
        }
    }
}