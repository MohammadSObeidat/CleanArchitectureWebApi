using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Exceptions
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        IProblemDetailsService problemDetailsService;
        public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            this.problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails problem = exception switch
            {
                ValidationException ex => new ValidationProblemDetails(
                    ex.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        ))
                {
                    Title = "Validation Failed",
                    Status = StatusCodes.Status400BadRequest,
                },

                DomainValidationException ex => new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                },

                NotFoundException ex => new ProblemDetails
                {
                    Title = "Resource not found!",
                    Detail = ex.Message,
                    Status = StatusCodes.Status404NotFound
                },

                //ForbiddenAccessException ex => new ProblemDetails
                //{
                //    Title = "Access Denied",
                //    Detail = ex.Message,
                //    Status = StatusCodes.Status403Forbidden
                //},

                _ => new ProblemDetails
                {
                    Title = "Server Error",
                    Detail = "An unhandled exception occurred.",
                    Status = StatusCodes.Status500InternalServerError
                }
            };

            httpContext.Response.StatusCode = problem.Status!.Value;

            await problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem,
            });

            return true;
        }
    }
}
