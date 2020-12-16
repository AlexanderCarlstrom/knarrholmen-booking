using api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Contexts
{
    public class BookingDbContext : IdentityDbContext<User>
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OpenHours> OpenHours { get; set; }
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
            
        }
    }
}