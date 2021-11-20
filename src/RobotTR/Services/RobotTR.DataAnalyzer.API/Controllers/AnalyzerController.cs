using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotTR.DataAnalyzer.API.Models;
using RobotTR.WebAPI.Core.Controllers;

namespace RobotTR.DataAnalyzer.API.Controllers
{
    [Authorize, Route("api/Analyzer")]
    public class AnalyzerController : MainController
    {
        [HttpGet("Anayze")]
        public IActionResult Analyze(AnalyzerRequestViewModel body)
        {

            return Ok();
        }
    }
}