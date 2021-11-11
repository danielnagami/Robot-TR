using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Models
{
    public class JobViewModel
    {
        public string Title { get; set; }
        public LevelEnum Level { get; set; }
        public ICollection<LanguagesEnum> Languages { get; set; }
        public ICollection<FrameworksEnum> Frameworks { get; set; }
        public Guid OwnerId { get; set; }
    }

    public enum LanguagesEnum
    {
        CSharp = 0,
        JavaScript = 1,
        HTML = 2,
        CSS = 3
    }

    public enum LevelEnum
    {
        Junior = 0,
        Middle = 1,
        Senior = 2
    }

    public enum FrameworksEnum
    {
        Angular = 0,
        React = 1,
        Node = 2,
    }
}
