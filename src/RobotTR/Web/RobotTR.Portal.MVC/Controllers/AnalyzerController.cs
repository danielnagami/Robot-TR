using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using RobotTR.Portal.MVC.Models;
using RobotTR.Portal.MVC.Services;
using RobotTR.WebAPI.Core.Controllers;
using RobotTR.WebAPI.Core.User;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Controllers
{
    [Route("[controller]")]
    public class AnalyzerController : MainController
    {
        public IJobsService _jobsService { get; set; }
        public IAspNetUser _aspNetUser { get; set; }

        public AnalyzerController(IJobsService jobsService, IAspNetUser aspNetUser)
        {
            _jobsService = jobsService;
            _aspNetUser = aspNetUser;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            var jobs = await _jobsService.GetJobs(_aspNetUser.GetUserId());
            var jobsList = jobs.ToList();

            ViewBag.Jobs = jobsList.Select(c => new SelectListItem()
            { Text = c.Title, Value = c.Id.ToString() }).ToList();

            return View("GetResult");
        }

        [HttpPost("GetResult")]
        public IActionResult GetResult([FromForm] AnalyzerRequestViewModel body)
        {
            var analyzedResult = new AnalyzerResultViewModel()
            {
                Score = "812",
                Message = "Segundo o portifólio do candidato, ele se enquadra como Sênior."
            };
            return View("Result", analyzedResult);
        }
    }
}