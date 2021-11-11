using Microsoft.AspNetCore.Mvc;
using RobotTR.Portal.MVC.Models;
using RobotTR.WebAPI.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Controllers
{
    [Route("[controller]")]
    public class AnalyzerController : MainController
    {
        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            return View("GetResult");
        }

        //[HttpPost("GetResult")]
        //public IActionResult GetResult([FromForm]AnalyzerRequestViewModel body)
        //{
        //    return RedirectToAction("Result", "Analyzer");
        //}

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