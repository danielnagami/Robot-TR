namespace RobotTR.Portal.MVC.Models
{
    public class AnalyzerResultViewModel
    {
        public string Message { get; set; }
        public string Score { get; set; }
    }

    public class AnalyzerRequestViewModel
    {
        public int ExperienceYears { get; set; }
        public string GithubUsername { get; set; }
    }
}