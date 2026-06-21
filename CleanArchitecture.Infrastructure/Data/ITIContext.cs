using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data
{
    public class ITIContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ITIContext(DbContextOptions<ITIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DepartmentConfiguration().Configure(modelBuilder.Entity<Department>());

            new EmployeeConfiguration().Configure(modelBuilder.Entity<Employee>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
