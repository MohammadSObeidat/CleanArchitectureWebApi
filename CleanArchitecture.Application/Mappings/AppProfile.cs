using AutoMapper;
using CleanArchitecture.Application.DTOs.Department;
using CleanArchitecture.Application.DTOs.Employee;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mappings
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            // ── Department ──────────────────────────────────────────────
            
            CreateMap<Department, DepartmentDto>();

            CreateMap<Department, DepartmentDetailsDto>();

            CreateMap<CreateDepartmentDto, Department>();

            //CreateMap<UpdateDepartmentCommand, Department>();

            //CreateMap<CreateDepartmentDto, CreateDepartmentCommand>();

            // ── Employee ────────────────────────────────────────────────

            CreateMap<Employee, EmployeeDto>();

            CreateMap<Employee, EmployeeDetailsDto>();

            CreateMap<CreateEmployeeDto, Employee>();

            //CreateMap<CreateEmployeeDto, CreateEmployeeCommand>();
        }
    }
}
