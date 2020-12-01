using System.Threading.Tasks;
using booking_api.Models;
using booking_api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace booking_api.Controllers
{
    [Route("users")]
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
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.LoginUserAsync(model);
            return StatusCode(response.StatusCode, response);
        }
    }
}