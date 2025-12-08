using EmployeeManagementProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
    }
}
