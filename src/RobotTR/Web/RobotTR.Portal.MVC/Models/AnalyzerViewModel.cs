using System;
using System.ComponentModel.DataAnnotations;

namespace RobotTR.Portal.MVC.Models
{
    public class AnalyzerResponseViewModel
    {
        public string Message { get; set; }
        public int Score { get; set; }
    }

    public class AnalyzerRequestViewModel
    {
        [Required(ErrorMessage = "A vaga não pode ser vazia.")]
        public string Job { get; set; }
        [Required(ErrorMessage = "Os anos de experiência não podem ser vazios.")]
        [Range(0, 150, ErrorMessage = "Os anos de experiência tem que ser igual ou maior que 0.")]
        public int ExperienceYears { get; set; }
        [Required(ErrorMessage = "O usuário do Github não pode ser vazio.")]
        public string GithubUsername { get; set; }
    }
}