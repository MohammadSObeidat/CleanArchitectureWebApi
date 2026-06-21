using CleanArchitecture.API.Exceptions;
using CleanArchitecture.Application.Common.Behaviors;
using CleanArchitecture.Application.Features.Departments.Commands.CreateDepartment;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repository;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Infrastructure.UnitOfWork;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Global Exception Handler Middleware
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Database
builder.Services.AddDbContext<ITIContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Service
builder.Services.AddScoped<IFileService, FileService>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateDepartmentCommand).Assembly);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(AppProfile).Assembly);

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateDepartmentCommandValidator>();
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

app.UseCors("ApiCorsPolicy");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
