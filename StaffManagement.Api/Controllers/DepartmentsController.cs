using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Application.Departments.Queries.GetAllDepartments;
using StaffManagement.Application.Departments.Queries.GetDepartmentById;

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

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
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
    }
}
