using CleanArchitecture.Application.DTOs.Employee;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters.");

            RuleFor(x => x.Salary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Salary cannot be negative.")
                .PrecisionScale(18, 2, false)
                .WithMessage("Salary must have up to 18 digits and 2 decimal places.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage("A valid department must be selected.");
        }
    }
}
