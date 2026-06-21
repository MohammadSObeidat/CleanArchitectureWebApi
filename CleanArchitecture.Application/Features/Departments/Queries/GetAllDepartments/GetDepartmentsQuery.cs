using CleanArchitecture.Application.DTOs.Department;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Queries.GetAllDepartments
{
    public sealed record GetDepartmentsQuery : IRequest<List<DepartmentDto>>;
}
