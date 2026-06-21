using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Commands.DeleteEmployee
{
    public sealed record DeleteEmployeeCommand(int Id) : IRequest;
}
