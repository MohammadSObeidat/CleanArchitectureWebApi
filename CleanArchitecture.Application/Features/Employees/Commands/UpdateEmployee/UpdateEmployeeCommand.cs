using CleanArchitecture.Application.DTOs.Employee;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Employees.Commands.UpdateEmployee
{
    //public sealed record UpdateEmployeeCommand(
    //    int Id,
    //    string FirstName,
    //    string LastName,
    //    decimal Salary,
    //    IFormFile? Image,
    //    int DepartmentId) : IRequest;

    public sealed record UpdateEmployeeCommand(int Id, UpdateEmployeeDto UpdateEmployeeDto) : IRequest;
}
