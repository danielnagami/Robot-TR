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
        public IActionResult Create([FromForm]JobViewModel body)
        {
            return View("Index");
        }
    }
}