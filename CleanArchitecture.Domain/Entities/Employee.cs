using CleanArchitecture.Domain.Common.Exceptions;

namespace CleanArchitecture.Domain.Entities
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public decimal Salary { get; private set; }
        public string? ImageUrl { get; private set; }
        public int DepartmentId { get; private set; }
        public Department? Department { get; private set; }

        private Employee() { }

        public Employee(string firstName, string lastName, decimal salary, string? imageUrl, int departmentId)
        {
            UpdateName(firstName, lastName);
            UpdateSalary(salary);
            UpdateImage(imageUrl);
            UpdateDepartment(departmentId);
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new DomainValidationException("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new DomainValidationException("Last name is required.");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public void UpdateSalary(decimal salary)
        {
            if (salary < 0)
            {
                throw new DomainValidationException("Salary cannot be negative.");
            }

            Salary = salary;
        }

        public void UpdateImage(string? imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public void UpdateDepartment(int departmentId)
        {
            if (departmentId <= 0)
            {
                throw new DomainValidationException("A valid department must be selected.");
            }

            DepartmentId = departmentId;
        }
    }
}
