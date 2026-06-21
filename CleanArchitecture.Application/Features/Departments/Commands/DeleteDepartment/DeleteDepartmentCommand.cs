using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Commands.DeleteDepartment
{
    public sealed record DeleteDepartmentCommand(int Id) : IRequest;
}
