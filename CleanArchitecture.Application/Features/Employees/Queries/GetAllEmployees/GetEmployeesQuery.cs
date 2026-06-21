using CleanArchitecture.Application.DTOs.Employee;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Queries.GetAllEmployees
{
    public sealed record GetEmployeesQuery() : IRequest<List<EmployeeDto>>;
}
