using CleanArchitecture.Application.DTOs.Employee;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Queries.GetAllEmployees
{
    public sealed record GetEmployeesQuery(
        string? FullName,
        bool SortSalaryAscending = true) : IRequest<List<EmployeeDto>>;
}
