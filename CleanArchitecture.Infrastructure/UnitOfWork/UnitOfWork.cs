using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITIContext context;
        public IRepository<Department> Departments { get; }
        public IRepository<Employee> Employees { get; }

        public UnitOfWork(ITIContext context, IRepository<Department> departments, IRepository<Employee> employees)
        {
            this.context = context;
            this.Departments = departments;
            this.Employees = employees;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
