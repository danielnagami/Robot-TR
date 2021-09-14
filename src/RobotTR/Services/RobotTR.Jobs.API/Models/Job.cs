using RobotTR.Core.DomainObjects;
using RobotTR.Core.Models;

namespace RobotTR.Jobs.API.Models
{
    public class Job : Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public LevelEnum Level { get; set; }
        public LanguagesEnum Languages { get; set; }
        public FrameworksEnum Frameworks { get; set; }
        public User Owner { get; set; }
    }
}