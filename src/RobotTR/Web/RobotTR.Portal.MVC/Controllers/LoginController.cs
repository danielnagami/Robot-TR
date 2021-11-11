using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RobotTR.Portal.MVC.Models;
using RobotTR.WebAPI.Core.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Controllers
{
    public class LoginController : MainController
    {
        public Services.IAuthenticationService _authenticationService { get; set; }

        public LoginController(Services.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm]UserLogin userLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(userLogin);

            var response = await _authenticationService.Login(userLogin);

            if (ResponseHasErrors(response.ResponseResult)) return View(userLogin);

            await LoginUser(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var response = await _authenticationService.Register(userRegister);

            if (ResponseHasErrors(response.ResponseResult)) return View(userRegister);

            await LoginUser(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task LoginUser(UserLoginResponse response)
        {
            var token = GetTokenFormatted(response.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.Add(new Claim("UserId", response.UsuarioToken.Id));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken GetTokenFormatted(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}