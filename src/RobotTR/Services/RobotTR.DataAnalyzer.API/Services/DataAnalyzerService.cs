using Microsoft.Extensions.Options;

//using Newtonsoft.Json;

using RobotTR.DataAnalyzer.API.Models;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RobotTR.DataAnalyzer.API.Services
{
    public class DataAnalyzerService : IDataAnalyzerService
    {
        public HttpClient _httpClient { get; set; }

        public DataAnalyzerService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            httpClient.BaseAddress = new System.Uri(options.Value.PythonAPIURL);

            _httpClient = httpClient;
        }

        public async Task<AnalyzerResponseViewModel> Analyze(AnalyzerRequestViewModel body)
        {
            var response = await _httpClient.GetAsync($"Analyze/{body.Job}/{body.ExperienceYears}/{body.GithubUsername}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,

            };

            return JsonSerializer.Deserialize<AnalyzerResponseViewModel>(await response.Content.ReadAsStringAsync(), options);
            //return JsonConvert.DeserializeObject<AnalyzerResponseViewModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
