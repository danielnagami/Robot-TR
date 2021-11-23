using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RobotTR.Portal.MVC.Models
{
    public class JobViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "O campo Nível é obrigatório.")]
        public LevelEnum Level { get; set; }
        [Required(ErrorMessage = "O campo Linguagens é obrigatório.")]
        public ICollection<LanguagesEnum> Languages { get; set; }
        [Required(ErrorMessage = "O campo Frameworks é obrigatório.")]
        public ICollection<FrameworksEnum> Frameworks { get; set; }
        public Guid OwnerId { get; set; }
        public string Owner { get; set; }
    }

    public enum LanguagesEnum
    {
        CSharp = 0,
        Java = 1,
    }

    public enum LevelEnum
    {
        Junior = 0,
        Middle = 1,
        Senior = 2
    }

    public enum FrameworksEnum
    {
        ASPNETMVC = 0,
        ASPNETCore = 1,
        EntityFramework = 2,
        Dapper = 3,
        NHibernate = 4,
        Http = 5,
        RabbitMQ = 6,
        Kafka = 7,
        Spring = 8,
        Play = 9,
        Struts = 10,
        Hibernate = 11,
        Wicket = 12,
        Spark = 13,
        GWT = 14
    }

    public class JobUpdate : JobViewModel
    {
        public IEnumerable<LanguagesEnum> AvailableLanguages { get; set; }
        public IEnumerable<FrameworksEnum> AvailableFrameworks { get; set; }
    }
}