using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Application.Employees.Commands.CreateEmployee;
using StaffManagement.Application.Employees.Dtos;
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

        [HttpGet]
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

        [HttpPost]
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
    }
}
