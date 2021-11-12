﻿using Microsoft.Extensions.Options;
using RobotTR.Portal.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public IList<FrameworksEnum> GetFrameworks()
        {
            return new List<FrameworksEnum>()
            { 
                FrameworksEnum.Angular, 
                FrameworksEnum.Node, 
                FrameworksEnum.React 
            };
        }

        public async Task<IEnumerable<JobViewModel>> GetJobs(Guid ownerId)
        {
            var response = await _httpClient.GetAsync($"/api/jobs/ReadAllByUser?ownerId={ownerId}");

            TreatErrors(response);

            return await DeserializeResponse<IEnumerable<JobViewModel>>(response);
            
        }

        public IList<LanguagesEnum> GetLanguages()
        {
            return new List<LanguagesEnum>()
            {
                LanguagesEnum.CSharp,
                LanguagesEnum.JavaScript,
                LanguagesEnum.HTML,
                LanguagesEnum.CSS
            };
        }
    }
}