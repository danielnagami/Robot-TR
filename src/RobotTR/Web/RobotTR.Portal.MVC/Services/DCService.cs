using Microsoft.Extensions.Options;
using RobotTR.Portal.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public class DCService : Service, IDCService
    {
        public HttpClient _httpClient { get; set; }

        public DCService(HttpClient httpClient, IOptions<Models.AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.DataCollectorURL);

            _httpClient = httpClient;
        }

        public async Task DropRepository(string repository)
        {
            await _httpClient.DeleteAsync($"/api/codes/delete-project?name={repository}");
        }

        public async Task<IList<RepositoryContentViewModel>> GetRepositoryContent(string repository, string username)
        {
            var response = await _httpClient.GetAsync($"/api/user/GetRepositoryContent?repository={repository}&username={username}");

            TreatErrors(response);

            return await DeserializeResponse<IList<RepositoryContentViewModel>>(response);
        }

        public async Task<IList<string>> GetUserRepositories(string username)
        {
            var response = await _httpClient.GetAsync($"/api/user/GetUsersRepositories?username={username}");

            TreatErrors(response);

            return await DeserializeResponse<IList<string>>(response);
        }
    }
}