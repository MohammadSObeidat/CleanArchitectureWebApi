using CleanArchitecture.Application.DTOs.Department;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentDtoValidator: AbstractValidator<CreateDepartmentDto>
    {
        public CreateDepartmentDtoValidator()
        {
            RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(50).WithMessage("Department name must not exceed 50 characters.");
        }
    }
}
