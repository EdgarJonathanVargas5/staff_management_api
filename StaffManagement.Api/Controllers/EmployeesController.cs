using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Domain.Errors;
using StaffManagement.Application.Employees.Dtos;
using StaffManagement.Application.Employees.Commands.CreateEmployee;
using StaffManagement.Application.Employees.Commands.DeleteEmployee;
using StaffManagement.Application.Employees.Commands.UpdateEmployee;
using StaffManagement.Application.Employees.Queries.GetAllEmployees;
using StaffManagement.Application.Employees.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Authorization;

namespace StaffManagement.Api.Controllers
{
    [Authorize]
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeesController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
        {
            var query = new GetAllEmployeesQuery();
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        [AllowAnonymous]
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

        [Authorize(Roles = "Admin")]
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
        
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
