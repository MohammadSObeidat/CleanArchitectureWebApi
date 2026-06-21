using CleanArchitecture.Application.DTOs.Department;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Commands.CreateDepartment
{
    //public sealed record CreateDepartmentCommand(string Name) : IRequest<int>;
    public sealed record CreateDepartmentCommand(CreateDepartmentDto CreateDepartmentDto) : IRequest<int>;

}
