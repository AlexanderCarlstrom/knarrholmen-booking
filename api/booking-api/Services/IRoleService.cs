using System.Linq;
using System.Threading.Tasks;
using booking_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace booking_api.Services
{
    public interface IRoleService
    {
        Task<Response> CreateRoleAsync(string name);
        Task<Response> DeleteRoleAsync(string id);
    }
}