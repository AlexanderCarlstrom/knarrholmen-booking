using System;
using System.Threading.Tasks;
using api.Models;
using api.Services;
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
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = await _authService.RegisterUserAsync(model);
            return StatusCode(response.Status, response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.LoginUserAsync(model);
            if (response.Success)
            {
                SetRefreshTokenCookie(response.RefreshToken.Token, response.RefreshToken.ExpiresAt);
            }

            return StatusCode(response.Status, response);
        }

        private void SetRefreshTokenCookie(string token, DateTime expires)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;
            cookieOptions.Expires = expires;
            Response.Cookies.Append("refresh-token", token, cookieOptions);
        }
    }
}