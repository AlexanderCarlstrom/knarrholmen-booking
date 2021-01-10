using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Responses;
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
            if (role != null) return new Response(400, name + " role already exist");

            var newRole = new IdentityRole(name);
            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded) return new Response(true, 201);

            var errors = result.Errors.Select(err => err.Description);
            return new Response(500, "Could not create role", errors);
        }

        public async Task<Response> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return new Response(400, "Role does not exist");

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) return new Response(true, 200);

            var response = new Response(400, "Could not create role");
            response.Errors = result.Errors.Select(err => err.Description);
            return response;
        }
    }
}