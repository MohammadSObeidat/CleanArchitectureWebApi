using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Configurations;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data
{
    public class ITIContext : IdentityDbContext<ApplicationUser> //DbContext
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
