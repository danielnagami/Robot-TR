using RobotTR.Core.Data;
using System;
using System.Collections.Generic;

namespace RobotTR.DataCollector.API.Models
{
    public interface ICodesRepository : IRepository<Codes>
    {
        void Add(Codes code);
        IList<Codes> GetByProjectId(Guid projectId);
        void DropProject(Guid projectId);
    }
}