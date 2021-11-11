using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RobotTR.Portal.MVC.Models;
using RobotTR.Portal.MVC.Services;
using RobotTR.WebAPI.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Controllers
{
    [Route("[controller]")]
    public class JobsController : MainController
    {
        public IJobsService _jobsService { get; set; }

        public JobsController(IJobsService jobsService)
        {
            _jobsService = jobsService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                userId = identity.FindFirst("UserId").Value;
            }

            //var jobsList = new List<JobViewModel>()
            //{
            //    { new JobViewModel()
            //    {
            //        Title = "Desenvolvedor .NET",
            //        Level = LevelEnum.Senior,
            //        OwnerId = Guid.NewGuid(),
            //        Languages = new List<LanguagesEnum>{{LanguagesEnum.CSharp}},
            //        Frameworks = new List<FrameworksEnum>{{FrameworksEnum.Angular}}

            //    } },

            //                    { new JobViewModel()
            //    {
            //        Title = "Desenvolvedor React",
            //        Level = LevelEnum.Middle,
            //        OwnerId = Guid.NewGuid(),
            //        Languages = new List<LanguagesEnum>{{LanguagesEnum.JavaScript}},
            //        Frameworks = new List<FrameworksEnum>{{FrameworksEnum.React}}

            //    } },
            //};

            var jobsList = await _jobsService.GetJobs(Guid.Parse(userId));
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
        public IActionResult Create([FromForm]JobViewModel body)
        {
            return View("Index");
        }
    }
}