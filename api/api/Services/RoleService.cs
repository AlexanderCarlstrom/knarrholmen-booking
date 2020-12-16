using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
    public interface IRoleService
    {
        Task<Response> CreateRoleAsync(string name);
        Task<Response> DeleteRoleAsync(string id);
    }

    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Response> CreateRoleAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role != null) return Response.BadRequest(name + " role already exist");

            var newRole = new IdentityRole(name);
            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded) return Response.Created(name + " role created successfully");

            var response = Response.BadRequest("Could not create role");
            response.Errors = result.Errors.Select(err => err.Description);
            return response;
        }

        public async Task<Response> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return Response.BadRequest("Role does not exist");

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) return Response.Ok();

            var response = Response.BadRequest("Could not create role");
            response.Errors = result.Errors.Select(err => err.Description);
            return response;
        }
    }
}