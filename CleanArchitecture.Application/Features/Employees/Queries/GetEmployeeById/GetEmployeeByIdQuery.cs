using CleanArchitecture.Application.DTOs.Employee;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Queries.GetEmployeeById
{
    public sealed record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDetailsDto>;
}
