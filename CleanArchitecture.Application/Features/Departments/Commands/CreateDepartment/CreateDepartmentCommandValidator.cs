using FluentValidation;

namespace CleanArchitecture.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandValidator: AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(d => d.CreateDepartmentDto.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(50).WithMessage("Department name must not exceed 50 characters.");
        }
    }
}
