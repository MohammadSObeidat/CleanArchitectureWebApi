using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Account;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Result> RegisterAsync(RegisterDto registerDto)
        {
            var validation = new RegisterDtoValidator();

            var validationResult = await validation.ValidateAsync(registerDto);

            if (!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var existingUser = await userManager.FindByEmailAsync(registerDto.Email);

            if (existingUser is not null)
            {
                return Result.Failure("Email is already in use.");
            }

            ApplicationUser user = new ApplicationUser();
            user.FirstName = registerDto.FirstName;
            user.LastName = registerDto.LastName;
            user.Address = registerDto.Address;
            user.Email = registerDto.Email;
            user.UserName = registerDto.Email;

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(e => e.Description));
            }

            // Add Role
            if (!await roleManager.RoleExistsAsync("User"))
            {
                return Result.Failure("Role User does not exist.");
            }

            await userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(e => e.Description));
            }

            return Result.Success();
        }

        public async Task<Result<AccountResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var validation = new LoginDtoValidator();

            var validationResult = await validation.ValidateAsync(loginDto);

            if (!validationResult.IsValid)
            {
                return Result<AccountResponseDto>.Failure(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            // Check if user exists
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return Result<AccountResponseDto>.Failure("Invalid Email or Password");
            }

            bool signInResult = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!signInResult)
            {
                return Result<AccountResponseDto>.Failure("Invalid Email or Password");
            }

            // ============= { Generate Token } =============

            // Step 1: Create claims that represent the authenticated user's identity.
            // These claims will be embedded inside the JWT.

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Step 2: Create the symmetric security key used to sign the JWT.
            // This key must match the key used in JWT validation middleware.

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"));

            // Step 3: Define the signing credentials.
            // This specifies the algorithm used to sign the token.

            SigningCredentials creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            // Step 4: Create the JWT token.
            var token = new JwtSecurityToken(
                            issuer: "WebApi",
                            audience: "WebApi",
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds
                        );

            // Step 5: Return the serialized JWT token to the client.
            // The client will send this token with future requests.

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Result<AccountResponseDto>.Success(new AccountResponseDto(tokenString));
        }

        public async Task<Result> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            bool roleExists = await roleManager.RoleExistsAsync(createRoleDto.RoleName);

            if (roleExists)
            {
                return Result.Failure($"Role '{createRoleDto.RoleName}' already exists.");
            }

            IdentityRole role = new IdentityRole();
            role.Name = createRoleDto.RoleName;

            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(e => e.Description));
            }

            return Result.Success();
        }
    }
}
