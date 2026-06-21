using CleanArchitecture.Application.DTOs.Department;
using CleanArchitecture.Application.Features.Departments.Commands.CreateDepartment;
using CleanArchitecture.Application.Features.Departments.Commands.DeleteDepartment;
using CleanArchitecture.Application.Features.Departments.Commands.UpdateDepartment;
using CleanArchitecture.Application.Features.Departments.Queries.GetAllDepartments;
using CleanArchitecture.Application.Features.Departments.Queries.GetDepartmentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator mediator;

        public DepartmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> Get()
        {
            var departments = await mediator.Send(new GetDepartmentsQuery());

            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentDetailsDto>> Get(int id)
        {
            var department =  await mediator.Send(new GetDepartmentByIdQuery(id));

            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateDepartmentDto createDepartmentDto)
        {
            var command = new CreateDepartmentCommand(createDepartmentDto);

            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentDto updateDepartmentDto)
        {
            var command = new UpdateDepartmentCommand(id, updateDepartmentDto);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentCommand(id);

            await mediator.Send(command);

            return NoContent();
        }
    }
}