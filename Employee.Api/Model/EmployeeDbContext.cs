using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Model
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public DbSet<EmployeeData> Employees { get; set; }

    }
}
