using CleanArchitecture.Application.DTOs.Department;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Commands.UpdateDepartment
{
    //public sealed record UpdateDepartmentCommand(int Id, string Name) : IRequest;
    public sealed record UpdateDepartmentCommand(int Id, UpdateDepartmentDto UpdateDepartmentDto) : IRequest;
}
