using RobotTR.Core.DomainObjects;
using System;

namespace RobotTR.DataCollector.API.Models
{
    public class Codes : Entity, IAggregateRoot
    {
        public Codes(Guid id, string project, string name, string content)
        {
            Id = id;
            Project = project;
            Name = name;
            Content = content;
        }
        protected Codes() { }
        public Guid Id { get; set; }
        public string Project { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}