using Microsoft.EntityFrameworkCore;
using school_api.Model;

namespace school_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<School> School { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
