using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotTR.DataAnalyzer.API.Models;
using RobotTR.WebAPI.Core.Controllers;

namespace RobotTR.DataAnalyzer.API.Controllers
{
    [Route("api/Analyzer")]
    public class AnalyzerController : MainController
    {
        [HttpPost("Analyze")]
        public IActionResult Analyze(AnalyzerRequestViewModel body)
        {
            var mock = new AnalyzerResponseViewModel
            {
                Score = 800,
                Message = "O perfil do candidato corresponde com o nível de Sênior."
            };

            return CustomResponse(mock);
        }
    }
}