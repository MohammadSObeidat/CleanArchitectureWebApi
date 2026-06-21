using CleanArchitecture.Application.DTOs.Employee;
using CleanArchitecture.Application.Features.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Features.Employees.Commands.DeleteEmployee;
using CleanArchitecture.Application.Features.Employees.Commands.UpdateEmployee;
using CleanArchitecture.Application.Features.Employees.Queries.GetAllEmployees;
using CleanArchitecture.Application.Features.Employees.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> Get()
        {
            List<EmployeeDto> employees = await mediator.Send(new GetEmployeesQuery());

            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDetailsDto>> Get(int id)
        {
            EmployeeDetailsDto employee = await mediator.Send(new GetEmployeeByIdQuery(id));

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateEmployeeDto createEmployeeDto)
        {
            var command = new CreateEmployeeCommand(createEmployeeDto);

            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateEmployeeDto updateEmployeeDto)
        {
            var command = new UpdateEmployeeCommand(id, updateEmployeeDto);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteEmployeeCommand(id);

            await mediator.Send(command);

            return NoContent();
        }
    }
}
