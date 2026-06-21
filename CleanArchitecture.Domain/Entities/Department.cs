using CleanArchitecture.Domain.Common.Exceptions;

namespace CleanArchitecture.Domain.Entities
{
    public class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public List<Employee>? Employees { get; private set; }

        private Department() { }

        public Department(string name)
        {
            UpdateName(name);
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainValidationException("Department name is required.");
            }

            Name = name;
        }
    }
}
