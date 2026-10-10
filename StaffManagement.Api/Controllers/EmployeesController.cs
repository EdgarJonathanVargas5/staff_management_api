using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Domain.Errors;
using StaffManagement.Application.Employees.Dtos;
using StaffManagement.Application.Employees.Commands.CreateEmployee;
using StaffManagement.Application.Employees.Commands.DeleteEmployee;
using StaffManagement.Application.Employees.Commands.UpdateEmployee;
using StaffManagement.Application.Employees.Queries.GetAllEmployees;
using StaffManagement.Application.Employees.Queries.GetEmployeeById;

namespace StaffManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
        {
            var query = new GetAllEmployeesQuery();
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            var result = await _sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateEmployeeCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                TaxId = request.TaxId
            };

            var result = await _sender.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return CreatedAtRoute(
                        "GetEmployeeById",
                        new { id = result.Value!.Id },
                        result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id:int}", Name = "UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateEmployeeCommand
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                TaxId = request.TaxId
            };

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                if (result.Errors.Any(error => error.Code == EmployeeErrors.NotFound.Code))
                {
                    return NotFound(result.Errors);
                }

                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Errors);
            }

            return NoContent();
        }
    }
}
