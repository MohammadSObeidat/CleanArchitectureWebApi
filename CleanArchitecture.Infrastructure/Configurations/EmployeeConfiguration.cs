using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(e => e.Salary)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.Department)
                  .WithMany(d => d.Employees)
                  .HasForeignKey(e => e.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
