using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.DTOs.Employee;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Employees.Queries.GetEmployeeById
{
    public sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailsDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<EmployeeDetailsDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            Employee? employee = await unitOfWork.Employees.GetByIdAsync(
                e => e.Id == request.Id, e => e.Department);

            if (employee is null)
            {
                throw new NotFoundException("Employee", request.Id);
            }

            EmployeeDetailsDto employeeDetailsDto = mapper.Map<EmployeeDetailsDto>(employee);

            return employeeDetailsDto;
        }
    }
}
