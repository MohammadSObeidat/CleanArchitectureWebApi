using CleanArchitecture.Application.DTOs.Department;
using MediatR;

namespace CleanArchitecture.Application.Features.Departments.Queries.GetDepartmentById
{
    public sealed record GetDepartmentByIdQuery(int Id): IRequest<DepartmentDetailsDto>;
}
