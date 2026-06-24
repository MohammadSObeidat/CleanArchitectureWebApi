using CleanArchitecture.Application.DTOs.Employee;
using CleanArchitecture.Application.Features.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Features.Employees.Commands.DeleteEmployee;
using CleanArchitecture.Application.Features.Employees.Commands.UpdateEmployee;
using CleanArchitecture.Application.Features.Employees.Queries.GetAllEmployees;
using CleanArchitecture.Application.Features.Employees.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> Get(string? fullName, bool sortSalaryAscending = true)
        {
            var query = new GetEmployeesQuery(fullName, sortSalaryAscending);

            List <EmployeeDto> employees = await mediator.Send(query);

            return Ok(employees);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDetailsDto>> Get(int id)
        {
            /*
            var authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var authenticatedUserRole = User.FindFirstValue(ClaimTypes.Role);

            var query = new GetEmployeeByIdQuery(id, authenticatedUserId, authenticatedUserRole);

            EmployeeDetailsDto employee = await mediator.Send(query);

            return Ok(employee);
            */

            EmployeeDetailsDto employee = await mediator.Send(new GetEmployeeByIdQuery(id));

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateEmployeeDto createEmployeeDto)
        {
            var command = new CreateEmployeeCommand(createEmployeeDto);

            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateEmployeeDto updateEmployeeDto)
        {
            var command = new UpdateEmployeeCommand(id, updateEmployeeDto);

            await mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteEmployeeCommand(id);

            await mediator.Send(command);

            return NoContent();
        }
    }
}
