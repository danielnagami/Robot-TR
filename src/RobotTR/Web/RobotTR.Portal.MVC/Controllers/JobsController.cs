using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using RobotTR.Portal.MVC.Models;
using RobotTR.Portal.MVC.Services;
using RobotTR.WebAPI.Core.Controllers;
using RobotTR.WebAPI.Core.User;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Controllers
{
    [Authorize, Route("[controller]")]
    public class JobsController : MainController
    {
        private IJobsService _jobsService;
        private IAspNetUser _aspNetUser;

        public JobsController(IJobsService jobsService, IAspNetUser aspNetUser)
        {
            _jobsService = jobsService;
            _aspNetUser = aspNetUser;
        }

        public async Task<IActionResult> Index()
        {
            var jobsList = await _jobsService.GetJobs(_aspNetUser.GetUserId());
            jobsList.ToList().ForEach(j => j.Owner = _aspNetUser.GetUserName());
            return View("Index", jobsList);
        }

        [HttpGet, Route("Create")]
        public IActionResult Create()
        {
            ViewBag.Languages = _jobsService.GetLanguages()
                .Select(c => new SelectListItem()
                {
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToList();

            ViewBag.Frameworks = _jobsService.GetFrameworks()
                .Select(c => new SelectListItem()
                {
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToList();

            return View();
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create([FromForm] JobViewModel body)
        {
            body.OwnerId = _aspNetUser.GetUserId();
            await _jobsService.Create(body);

            return RedirectToAction("Index");
        }

        [HttpGet("Read")]
        public async Task<IActionResult> Read(Guid jobId)
        {
            var job = await _jobsService.Read(jobId);
            return View("Read", job);
        }

        [HttpGet("Update")]
        public IActionResult Update()
        {
            ViewBag.Languages = _jobsService.GetLanguages()
                .Select(c => new SelectListItem()
                {
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToList();

            ViewBag.Frameworks = _jobsService.GetFrameworks()
                .Select(c => new SelectListItem()
                {
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToList();

            return View();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Edit(JobViewModel job)
        {
            await _jobsService.Edit(job);
            return RedirectToAction("Index");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid jobId)
        {
            await _jobsService.Delete(jobId);
            return RedirectToAction("Index");
        }
    }
}