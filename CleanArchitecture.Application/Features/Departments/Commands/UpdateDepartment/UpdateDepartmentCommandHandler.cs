using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Commands.UpdateDepartment
{
    public sealed class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department? department = await unitOfWork.Departments.GetByIdAsync(d => d.Id == request.Id);

            if (department is null)
            {
                throw new NotFoundException("Department", request.Id);
            }

            //department.UpdateName(request.Name);
            department.UpdateName(request.UpdateDepartmentDto.Name);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
