using AutoMapper;
using CleanArchitecture.Application.DTOs.Department;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Queries.GetAllDepartments
{
    public sealed class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetDepartmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Department> departments = await unitOfWork.Departments.GetAllAsync();

            List<DepartmentDto> departmentsDto = mapper.Map<List<DepartmentDto>>(departments);

            return departmentsDto;
        }
    }
}
