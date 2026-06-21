using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Department> Departments { get; }
        IRepository<Employee> Employees { get; }
        Task<int> SaveChangesAsync();
    }
}
