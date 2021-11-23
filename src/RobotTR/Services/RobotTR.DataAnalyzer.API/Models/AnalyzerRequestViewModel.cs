using System;

namespace RobotTR.DataAnalyzer.API.Models
{
    public class AnalyzerRequestViewModel
    {
        public Guid Job { get; set; }
        public int ExperienceYears { get; set; }
        public string GithubUsername { get; set; }
    }

    public class AnalyzerResponseViewModel
    {
        public float Score { get; set; }
        public string Mensagem { get; set; }
    }
}
