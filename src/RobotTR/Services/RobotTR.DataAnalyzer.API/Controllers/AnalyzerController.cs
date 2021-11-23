using Microsoft.AspNetCore.Mvc;

using RobotTR.DataAnalyzer.API.Models;
using RobotTR.DataAnalyzer.API.Services;
using RobotTR.WebAPI.Core.Controllers;

using System.Threading.Tasks;

namespace RobotTR.DataAnalyzer.API.Controllers
{
    [Route("api/Analyzer")]
    public class AnalyzerController : MainController
    {
        public IDataAnalyzerService _dataAnalyzerService { get; set; }

        public AnalyzerController(IDataAnalyzerService dataAnalyzerService)
        {
            _dataAnalyzerService = dataAnalyzerService;
        }

        [HttpPost("Analyze")]
        public async Task<IActionResult> Analyze(AnalyzerRequestViewModel body)
        {
            var response = await _dataAnalyzerService.Analyze(body);

            response.Mensagem = "O perfil do candidato corresponde ao nível de Júnior.";

            if (response.Score > 500)
                response.Mensagem = "O perfil do candidato corresponde ao nível de Pleno.";
            if (response.Score > 800)
                response.Mensagem = "O perfil do candidato corresponde ao nível de Senior.";

            return CustomResponse(response);
        }
    }
}