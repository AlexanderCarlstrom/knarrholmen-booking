using System;
using System.Threading.Tasks;
using api.Contracts.Requests;
using api.Services;
using api.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var response = await _authService.RegisterUserAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var response = await _authService.LoginUserAsync(model);
            if (response.Success && model.Remember)
            {
                SetRefreshTokenCookie(response.RefreshToken.Token, response.RefreshToken.ExpiresAt);
            }

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("login-with-token")]
        public async Task<IActionResult> LoginWithToken()
        {
            var getToken = Request.Cookies.TryGetValue(Authorization.RefreshTokenCookieName, out var token);
            if (!getToken)
            {
                return StatusCode(401);
            }

            var response = await _authService.LoginWithTokenASync(token);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest model)
        {
            if (string.IsNullOrEmpty(model.UserId)) return BadRequest("User id is required");
            var refreshToken = Request.Cookies[Authorization.RefreshTokenCookieName];
            model.RefreshToken = refreshToken;
            var response =
                    await _authService.LogoutUserAsync(model);
            RemoveRefreshTokenCookie();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("unauthorized")]
        public IActionResult InvalidOrNoAccessToken()
        {
            Response.Headers.Add("token-expired", "true");
            return StatusCode(401);
        }

        [HttpGet]
        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return StatusCode(403);
        }

        private void SetRefreshTokenCookie(string token, DateTime expires)
        {
            var cookieOptions = new CookieOptions {HttpOnly = true, Expires = expires, IsEssential = true};
            Response.Cookies.Append(Authorization.RefreshTokenCookieName, token, cookieOptions);
        }

        private void RemoveRefreshTokenCookie()
        {
            var cookieOptions = new CookieOptions {HttpOnly = true, Expires = DateTime.Now.AddMinutes(1), IsEssential = true};
            Response.Cookies.Append(Authorization.RefreshTokenCookieName, "expired token", cookieOptions);
            Response.Cookies.Append("refresh-token", "expired token", cookieOptions);
        }
    }
}