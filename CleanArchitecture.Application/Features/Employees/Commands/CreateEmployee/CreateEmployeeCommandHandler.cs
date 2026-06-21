using AutoMapper;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Commands.CreateEmployee
{
    public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService fileService;
        private readonly IMapper mapper;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.fileService = fileService;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            string? imageUrl = null;

            if (request.CreateEmployeeDto.Image is not null)
            {
                imageUrl = await fileService.UploadFileAsync("uploads", request.CreateEmployeeDto.Image);
            }

            Employee employee = mapper.Map<Employee>(request.CreateEmployeeDto);

            employee.UpdateImage(imageUrl);

            await unitOfWork.Employees.InsertAsync(employee);

            await unitOfWork.SaveChangesAsync();

            return employee.Id;
        }
    }
}
