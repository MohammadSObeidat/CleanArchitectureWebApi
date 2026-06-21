using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Commands.DeleteDepartment
{
    public sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department? department = await unitOfWork.Departments.GetByIdAsync(d => d.Id == request.Id);

            if (department is null)
            {
                throw new NotFoundException("Department", request.Id);
            }

            unitOfWork.Departments.Delete(department);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
