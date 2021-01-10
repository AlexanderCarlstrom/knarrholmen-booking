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
            if (response.Success)
            {
                SetRefreshTokenCookie(response.RefreshToken.Token, response.RefreshToken.ExpiresAt);
            }

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(string userId)
        {
            var token = Request.Cookies[Authorization.RefreshTokenCookieName];
            var response =
                await _authService.LogoutUserAsync(new LogoutRequest {UserId = userId, RefreshToken = token});

            return StatusCode(response.StatusCode, response);
        }

        private void SetRefreshTokenCookie(string token, DateTime expires)
        {
            var cookieOptions = new CookieOptions {HttpOnly = true, Expires = expires, IsEssential = true};
            Response.Cookies.Append("refresh-token", token, cookieOptions);
        }
    }
}