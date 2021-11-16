using RobotTR.DataAnalyzer.API.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.DataAnalyzer.API.Services
{
    public interface IDCService
    {
        Task<List<string>> GetRepositories(string username);
        Task<RepositoryContentViewModel> GetRepositoryContent();
    }
}