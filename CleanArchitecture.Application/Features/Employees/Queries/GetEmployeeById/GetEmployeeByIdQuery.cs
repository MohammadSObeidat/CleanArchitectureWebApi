using CleanArchitecture.Application.DTOs.Employee;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Queries.GetEmployeeById
{
    public sealed record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDetailsDto>;

    //public sealed record GetEmployeeByIdQuery(
    //    int Id,
    //    string? AuthenticatedUserId,
    //    string? AuthenticatedUserRole) : IRequest<EmployeeDetailsDto>;
}
