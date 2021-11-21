using RobotTR.Portal.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public interface IDCService
    {
        Task<IList<string>> GetUserRepositories(string username);
        Task<IList<RepositoryContentViewModel>> GetRepositoryContent(string repository, string username);
        Task DropRepository(string repository);
    }
}