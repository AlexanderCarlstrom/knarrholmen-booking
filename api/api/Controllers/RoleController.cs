using System.Threading.Tasks;
using api.Models;
using booking_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("roles")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleModel model)
        {
            var result = await _roleService.CreateRoleAsync(model.Name);
            return StatusCode(result.StatusCode, result);
        }

        public async Task<IActionResult> Delete([FromBody] DeleteRoleModel model)
        {
            var result = await _roleService.DeleteRoleAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
    }
}