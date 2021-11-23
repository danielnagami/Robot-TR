using Microsoft.Extensions.Options;
using RobotTR.Portal.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public class JobsService : Service, IJobsService
    {
        private readonly HttpClient _httpClient;

        public JobsService(HttpClient httpClient, IOptions<Models.AppSettings> settings)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(settings.Value.JobsURL);
        }

        public async Task Create(JobViewModel job)
        {
            var response = await _httpClient.PostAsync($"/api/jobs/Create", 
                                                        new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json"));

            TreatErrors(response);
        }

        public async Task Delete(Guid jobId)
        {
            await _httpClient.DeleteAsync($"/api/jobs/Delete?jobId={jobId}");
        }

        public async Task Edit(JobViewModel job)
        {
            await _httpClient.PutAsync($"/api/jobs/Update",
                                        new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json"));
        }

        public async Task<JobViewModel> Read(Guid jobId)
        {
            var response = await _httpClient.GetAsync($"/api/jobs/Read?jobId={jobId}");

            TreatErrors(response);

            return await DeserializeResponse<JobViewModel>(response);
        }

        public async Task<IEnumerable<JobViewModel>> GetJobs(Guid ownerId)
        {
            var response = await _httpClient.GetAsync($"/api/jobs/ReadAllByUser?ownerId={ownerId}");

            TreatErrors(response);

            return await DeserializeResponse<IEnumerable<JobViewModel>>(response);
        }

        public IList<FrameworksEnum> GetFrameworks()
        {
            return new List<FrameworksEnum>()
            { 
                FrameworksEnum.ASPNETCore, 
                FrameworksEnum.ASPNETMVC, 
                FrameworksEnum.Dapper, 
                FrameworksEnum.EntityFramework, 
                FrameworksEnum.GWT, 
                FrameworksEnum.Hibernate,
                FrameworksEnum.Http, 
                FrameworksEnum.Kafka, 
                FrameworksEnum.NHibernate, 
                FrameworksEnum.Play, 
                FrameworksEnum.RabbitMQ, 
                FrameworksEnum.Spark, 
                FrameworksEnum.Spring,
                FrameworksEnum.Struts,
                FrameworksEnum.Wicket
            };
        }

        public IList<LanguagesEnum> GetLanguages()
        {
            return new List<LanguagesEnum>()
            {
                LanguagesEnum.CSharp,
                LanguagesEnum.Java
            };
        }
    }
}
