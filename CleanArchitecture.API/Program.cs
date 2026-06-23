using CleanArchitecture.API.Exceptions;
using CleanArchitecture.Application.Common.Behaviors;
using CleanArchitecture.Application.Features.Departments.Commands.CreateDepartment;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Repository;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Infrastructure.UnitOfWork;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // ===============================
    // 1) Define the JWT Bearer security scheme
    // ===============================
    //
    // This tells Swagger that our API uses JWT Bearer authentication
    // through the HTTP Authorization header.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // The name of the HTTP header where the token will be sent.
        Name = "Authorization",

        // Indicates this is an HTTP authentication scheme.
        Type = SecuritySchemeType.Http,

        // Specifies the authentication scheme name.
        // Must be exactly "Bearer" for JWT Bearer tokens.
        Scheme = "Bearer",

        // Optional metadata to describe the token format.
        BearerFormat = "JWT",

        // Specifies that the token is sent in the request header.
        In = ParameterLocation.Header,

        // Text shown in Swagger UI to guide the user.
        Description = "Enter: Bearer {your JWT token}"
    });

    // ===============================
    // 2) Require the Bearer scheme for secured endpoints
    // ===============================
    //
    // This tells Swagger that endpoints protected by [Authorize]
    // require the Bearer token defined above.
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                // Reference the previously defined "Bearer" security scheme.
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            // No scopes are required for JWT Bearer authentication.
            // This array is empty because JWT does not use OAuth scopes here.
            new string[] {}
        }
    });
});

// Database
builder.Services.AddDbContext<ITIContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;

    // User settings.
    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<ITIContext>().AddDefaultTokenProviders();

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "WebApi",

        ValidateAudience = true,
        ValidAudience = "WebApi",

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456")),

        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero
    };
});

// Authorization
builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7047"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Rate Limiting
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("login", opt =>
//    {
//        opt.PermitLimit = 5;
//        opt.Window = TimeSpan.FromMinutes(1);
//        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        opt.QueueLimit = 0;
//    });
//});



// Global Exception Handler Middleware
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Application Layer
// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateDepartmentCommand).Assembly);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(AppProfile).Assembly);

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateDepartmentDtoValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("ApiCorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
