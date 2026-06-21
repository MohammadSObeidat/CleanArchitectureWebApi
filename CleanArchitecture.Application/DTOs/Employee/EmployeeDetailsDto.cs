namespace CleanArchitecture.Application.DTOs.Employee
{
    public sealed record EmployeeDetailsDto(
        int Id, 
        string FirstName, 
        string LastName, 
        decimal Salary, 
        string? ImageUrl, 
        string DepartmentName);
}
