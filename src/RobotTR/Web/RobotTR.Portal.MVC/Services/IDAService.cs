using RobotTR.Portal.MVC.Models;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public interface IDAService
    {
        Task<AnalyzerResponseViewModel> Analyze(AnalyzerRequestViewModel body);
    }
}