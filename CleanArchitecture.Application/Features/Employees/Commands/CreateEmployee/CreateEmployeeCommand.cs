using CleanArchitecture.Application.DTOs.Employee;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Employees.Commands.CreateEmployee
{
    //public sealed record CreateEmployeeCommand(
    //    string FirstName,
    //    string LastName,
    //    decimal Salary,
    //    IFormFile? Image,
    //    int DepartmentId) : IRequest<int>;

    public sealed record CreateEmployeeCommand(CreateEmployeeDto CreateEmployeeDto) : IRequest<int>;
}
