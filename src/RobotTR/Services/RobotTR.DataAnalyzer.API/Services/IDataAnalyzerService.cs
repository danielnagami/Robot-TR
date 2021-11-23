using RobotTR.DataAnalyzer.API.Models;

using System.Threading.Tasks;

namespace RobotTR.DataAnalyzer.API.Services
{
    public interface IDataAnalyzerService
    {
        Task<AnalyzerResponseViewModel> Analyze(AnalyzerRequestViewModel body);
    }
}