using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RobotTR.Jobs.API.Models
{
    public class JobCreation
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public IList<LanguagesEnum> Languages { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public IList<FrameworksEnum> Frameworks { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public LevelEnum Level { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid OwnerId { get; set; }
    }
}