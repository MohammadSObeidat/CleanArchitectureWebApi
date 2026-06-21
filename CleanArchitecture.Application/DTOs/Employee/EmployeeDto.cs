namespace CleanArchitecture.Application.DTOs.Employee
{
    public sealed record EmployeeDto(
        int Id, 
        string FirstName, 
        string LastName, 
        decimal Salary, 
        string? ImageUrl, 
        string DepartmentName);
}
