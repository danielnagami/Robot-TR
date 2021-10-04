using RobotTR.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace RobotTR.Jobs.API.Models
{
    public class Job : Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public LevelEnum Level { get; set; }
        public ICollection<LanguagesEnum> Languages { get; set; }
        public ICollection<FrameworksEnum> Frameworks { get; set; }
        public Guid OwnerId { get; set; }
    }
}