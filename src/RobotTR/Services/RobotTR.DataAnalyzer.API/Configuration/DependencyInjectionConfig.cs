using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Polly;

using RobotTR.DataAnalyzer.API.Extensions;
using RobotTR.DataAnalyzer.API.Services;
using RobotTR.DataAnalyzer.API.Services.Handlers;
using RobotTR.WebAPI.Core.User;

using System;

namespace RobotTR.DataAnalyzer.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();


            services.AddHttpClient<IDataAnalyzerService, DataAnalyzerService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.WaitTry())
                    .AddTransientHttpErrorPolicy(
                        p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }
    }
}