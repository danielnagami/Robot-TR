using System;
using System.Security.Claims;

namespace RobotTR.WebAPI.Core.User
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                claim = principal.FindFirst("UserId");

            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("Username");
            return claim?.Value;
        }
    }
}