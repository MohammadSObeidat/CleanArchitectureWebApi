using FluentValidation;

namespace CleanArchitecture.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.CreateEmployeeDto.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.CreateEmployeeDto.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters.");

            RuleFor(x => x.CreateEmployeeDto.Salary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Salary cannot be negative.")
                .PrecisionScale(18, 2, false)
                .WithMessage("Salary must have up to 18 digits and 2 decimal places.");

            RuleFor(x => x.CreateEmployeeDto.DepartmentId)
                .GreaterThan(0)
                .WithMessage("A valid department must be selected.");
        }
    }
}
