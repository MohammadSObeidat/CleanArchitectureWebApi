using CleanArchitecture.Application.DTOs.Account;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await accountService.RegisterAsync(registerDto);

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        [EnableRateLimiting("AuthLimiter")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await accountService.LoginAsync(loginDto);

            if (result.IsFailure)
            {
                return Unauthorized(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            var result = await accountService.CreateRoleAsync(createRoleDto);

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return StatusCode(StatusCodes.Status201Created,
                new { Message = $"Role '{createRoleDto.RoleName}' created successfully." });
        }
    }
}
