using Microsoft.EntityFrameworkCore;

namespace Management.Services
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Models.Employee> Employees { get; set; }
    }
}
