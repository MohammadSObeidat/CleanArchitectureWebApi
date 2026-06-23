using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Account;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Result> RegisterAsync(RegisterDto registerDto);
        Task<Result<AccountResponseDto>> LoginAsync(LoginDto loginDto);
        Task<Result> CreateRoleAsync(CreateRoleDto createRoleDto);
    }
}
