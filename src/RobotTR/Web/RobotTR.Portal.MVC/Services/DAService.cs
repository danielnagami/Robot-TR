using Microsoft.Extensions.Options;
using RobotTR.Portal.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public class DAService : Service, IDAService
    {
        public HttpClient _httpClient { get; set; }

        public DAService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            httpClient.BaseAddress = new System.Uri(options.Value.DataAnalyzerURL);

            _httpClient = httpClient;
        }

        public async Task<AnalyzerResponseViewModel> Analyze(AnalyzerRequestViewModel body)
        {
            var response = await _httpClient.PostAsync($"/api/Analyzer/Analyze",
                                            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));

            TreatErrors(response);

            return await DeserializeResponse<AnalyzerResponseViewModel>(response);
        }
    }
}