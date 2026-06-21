using AutoMapper;
using CleanArchitecture.Application.DTOs.Employee;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Queries.GetAllEmployees
{
    public sealed class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees = await unitOfWork.Employees.GetAllAsync(includes: e => e.Department);

            List<EmployeeDto> employeesDto = mapper.Map<List<EmployeeDto>>(employees);

            return employeesDto;
        }
    }
}
