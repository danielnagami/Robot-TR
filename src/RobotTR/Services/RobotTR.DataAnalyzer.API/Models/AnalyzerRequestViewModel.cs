namespace RobotTR.DataAnalyzer.API.Models
{
    public class AnalyzerRequestViewModel
    {
        public string Job { get; set; }
        public int ExperienceYears { get; set; }
        public string GithubUsername { get; set; }
    }

    public class AnalyzerResponseViewModel
    {
        public int Score { get; set; }
        public string Message { get; set; }
    }
}
