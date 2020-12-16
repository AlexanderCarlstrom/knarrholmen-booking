using System.Threading.Tasks;
using api.Constants;
using api.Entities;
using Microsoft.AspNetCore.Identity;

namespace api.Contexts
{
    public class BookingDbContextSeed
    {
        public static async Task CreateDbSeed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create roles
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));

            // Create default admin
            var defaultAdmin = new User
                {FirstName = "Alexander", LastName = "Carlström", Email = "alexander@gmail.com", UserName = "alexander@gmail.com", EmailConfirmed = true};
            await userManager.CreateAsync(defaultAdmin, Authorization.DefaultPassword);
            await userManager.AddToRoleAsync(defaultAdmin, UserRoles.Admin.ToString());

            // Create default user
            var defaultUser = new User
                {FirstName = "Kim", LastName = "Sundström", Email = "kim@gmail.com", UserName = "kim@gmail.com", EmailConfirmed = true};
            await userManager.CreateAsync(defaultUser, Authorization.DefaultPassword);
            await userManager.AddToRoleAsync(defaultUser, UserRoles.Admin.ToString());
        }
    }
}