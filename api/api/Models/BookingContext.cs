using api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class BookingContext : IdentityDbContext<User>
    {
        public DbSet<Activity> Activities { get; set; }
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
            
        }
    }
}