using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.DTOs.Department;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Queries.GetDepartmentById
{
    public sealed class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDetailsDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetDepartmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<DepartmentDetailsDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Department? department = await unitOfWork.Departments.GetByIdAsync(d => d.Id == request.Id);

            if (department is null)
            {
                throw new NotFoundException("Department", request.Id);
            }

            DepartmentDetailsDto departmentDetailsDto = mapper.Map<DepartmentDetailsDto>(department);

            return departmentDetailsDto;
        }
    }
}
