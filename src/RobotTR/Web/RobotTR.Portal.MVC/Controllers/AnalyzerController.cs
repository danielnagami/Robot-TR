using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using RobotTR.Portal.MVC.Models;
using RobotTR.Portal.MVC.Services;
using RobotTR.Portal.MVC.Services.Handlers;
using RobotTR.WebAPI.Core.Controllers;
using RobotTR.WebAPI.Core.User;

using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AnalyzerController : MainController
    {
        public IJobsService _jobsService { get; set; }
        public IAspNetUser _aspNetUser { get; set; }
        public IDCService _dCService { get; set; }
        public IDAService _dAService { get; set; }

        public AnalyzerController(IJobsService jobsService, IAspNetUser aspNetUser, IDCService dCService, IDAService dAService)
        {
            _jobsService = jobsService;
            _aspNetUser = aspNetUser;
            _dCService = dCService;
            _dAService = dAService;
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
        public async Task<IActionResult> GetResult([FromForm] AnalyzerRequestViewModel body)
        {
            var repositories = await _dCService.GetUserRepositories(body.GithubUsername);

            Task.WaitAll(repositories.Select(repository => _dCService.GetRepositoryContent(repository, body.GithubUsername)).ToArray());

            var response = await _dAService.Analyze(body);

            repositories.ToList().ForEach(repo => _dCService.DropRepository(repo));

            #region .: Mock Response :.

            //var response = new AnalyzerResponseViewModel()
            //{
            //    Score = "812",
            //    Message = "Segundo o portifólio do candidato, ele se enquadra como Sênior."
            //};

            #endregion

            return View("Result", response);
        }
    }
}