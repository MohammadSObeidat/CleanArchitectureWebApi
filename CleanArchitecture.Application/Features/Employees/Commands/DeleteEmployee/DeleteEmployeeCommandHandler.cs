using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Commands.DeleteEmployee
{
    public sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? employee = await unitOfWork.Employees.GetByIdAsync(e => e.Id == request.Id);

            if (employee == null)
            {
                throw new NotFoundException("Employee", request.Id);
            }

            unitOfWork.Employees.Delete(employee);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
