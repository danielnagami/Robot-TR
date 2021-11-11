﻿using RobotTR.Portal.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public interface IJobsService
    {
        Task<IEnumerable<JobViewModel>> GetJobs(Guid ownerId);
        IList<LanguagesEnum> GetLanguages();
        IList<FrameworksEnum> GetFrameworks();
    }
}