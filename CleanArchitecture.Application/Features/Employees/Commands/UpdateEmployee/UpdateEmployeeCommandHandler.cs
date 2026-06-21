using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Commands.UpdateEmployee
{
    public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService fileService;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            this.fileService = fileService;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? employee = await unitOfWork.Employees.GetByIdAsync(e => e.Id == request.Id);

            if (employee is null)
            {
                throw new NotFoundException("Employee", request.Id);
            }

            // Keep old image by default
            string? imageUrl = employee.ImageUrl;

            if (request.UpdateEmployeeDto.Image is not null)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(employee.ImageUrl))
                {
                    await fileService.DeleteFileAsync("uploads", employee.ImageUrl);
                }

                // Upload new image
                imageUrl = await fileService.UploadFileAsync("uploads", request.UpdateEmployeeDto.Image);
            }

            employee.UpdateName(request.UpdateEmployeeDto.FirstName, request.UpdateEmployeeDto.LastName);
            employee.UpdateSalary(request.UpdateEmployeeDto.Salary);
            employee.UpdateImage(imageUrl);
            employee.UpdateDepartment(request.UpdateEmployeeDto.DepartmentId);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
