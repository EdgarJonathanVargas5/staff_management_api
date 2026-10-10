using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Domain.Errors;
using StaffManagement.Application.Departments.Dtos;
using StaffManagement.Application.Departments.Commands.CreateDepartment;
using StaffManagement.Application.Departments.Commands.UpdateDepartment;
using StaffManagement.Application.Departments.Queries.GetAllDepartments;
using StaffManagement.Application.Departments.Queries.GetDepartmentById;
using StaffManagement.Application.Departments.Commands.DeleteDepartment;

namespace StaffManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ISender _sender;

        public DepartmentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        [HttpGet("{id:int}", Name = "GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int id, CancellationToken cancellationToken)
        {
            var query = new GetDepartmentByIdQuery{ Id = id };
            var result = await _sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }

        [HttpPost(Name = "CreateDepartment")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateDepartmentCommand{ Name = request.Name };
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtRoute(
                        "GetDepartmentById",
                        new { id = result.Value!.Id },
                        result.Value);
        }

        [HttpPut("{id:int}", Name = "UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateDepartmentCommand { Id = id, Name = request.Name};
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                if (result.Errors.Any(error => error.Code == DepartmentErrors.NotFound.Code))
                {
                    return NotFound(result.Errors);
                }

                return BadRequest(result.Errors);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteDepartmentCommand { Id = id };
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
               return NotFound(result.Errors);
            }
            return NoContent();
        }
    }
}
