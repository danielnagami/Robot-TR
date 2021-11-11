using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RobotTR.Core.Communication;
using RobotTR.Portal.MVC.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient, IOptions<Models.AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationURL);

            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/auth/login", loginContent);

            if (!TreatErrors(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(response)
                };
            }

            return await DeserializeResponse<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(UserRegister userRegister)
        {
            var registroContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/auth/new-account", registroContent);

            if (!TreatErrors(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(response)
                };
            }

            return await DeserializeResponse<UserLoginResponse>(response);
        }
    }
}
