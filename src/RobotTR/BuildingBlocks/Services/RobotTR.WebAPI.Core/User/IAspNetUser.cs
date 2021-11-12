using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RobotTR.WebAPI.Core.User
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserName();
        string GetUserToken();
        bool IsAuthenticated();
        bool HasRole(string role);
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
    }
}