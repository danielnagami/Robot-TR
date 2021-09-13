using RobotTR.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.DataCollector.API.Models
{
    public interface ICodesRepository : IRepository<Codes>
    {
        void Add(Codes code);
        Task<IEnumerable<Codes>> GetAll();
        Task<List<Codes>> GetByName(string className);
        void DropProject(string projectName);
    }
}