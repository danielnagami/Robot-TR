using RobotTR.Portal.MVC.Models;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services.Handlers
{
    public class AnalyzerHandler
    {
        public IDCService _dCService { get; set; }
        public IDAService _dAService { get; set; }

        public AnalyzerHandler(IDCService dCService, IDAService dAService)
        {
            _dCService = dCService;
            _dAService = dAService;
        }

        public async Task<AnalyzerResponseViewModel> Analyze(AnalyzerRequestViewModel body)
        {
            var repositories = await _dCService.GetUserRepositories(body.GithubUsername);

            foreach (var repository in repositories)
            {
                await _dCService.GetRepositoryContent(repository, body.GithubUsername);
            }

            return await _dAService.Analyze(body);
        }
    }
}