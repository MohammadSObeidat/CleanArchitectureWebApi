namespace CleanArchitecture.Application.DTOs.Account
{
    public sealed record RegisterDto(
        string FirstName, 
        string LastName, 
        string Email, 
        string Address,
        string Password, 
        string ConfirmPassword);
}
