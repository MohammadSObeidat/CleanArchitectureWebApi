using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.DTOs.Employee
{
    public sealed record UpdateEmployeeDto(
       string FirstName,
       string LastName,
       decimal Salary,
       IFormFile? Image,  
       int DepartmentId);
}
