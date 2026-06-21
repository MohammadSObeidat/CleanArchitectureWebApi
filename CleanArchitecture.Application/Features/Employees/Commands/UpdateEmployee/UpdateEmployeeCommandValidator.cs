using FluentValidation;

namespace CleanArchitecture.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.UpdateEmployeeDto.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.UpdateEmployeeDto.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters.");

            RuleFor(x => x.UpdateEmployeeDto.Salary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Salary cannot be negative.")
                .PrecisionScale(18, 2, false)
                .WithMessage("Salary must have up to 18 digits and 2 decimal places.");

            RuleFor(x => x.UpdateEmployeeDto.DepartmentId)
                .GreaterThan(0)
                .WithMessage("A valid department must be selected.");
        }
    }
}
