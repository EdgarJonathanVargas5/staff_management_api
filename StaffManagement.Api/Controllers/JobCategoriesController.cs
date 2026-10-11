using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Application.JobCategories.Queries.GetAllJobCategories;
using StaffManagement.Application.JobCategories.Queries.GetJobCategoryById;

namespace StaffManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoriesController : ControllerBase
    {
        private readonly ISender _sender;

        public JobCategoriesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "GetAllJobCategories")]
        public async Task<IActionResult> GetAllJobCategories(CancellationToken cancellationToken)
        {
            var query = new GetAllJobCategoriesQuery();
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        [HttpGet("{id:int}", Name = "GetJobCategoryById")]
        public async Task<IActionResult> GetJobCategoryById(int id, CancellationToken cancellationToken)
        {
            var query = new GetJobCategoryByIdQuery{ Id = id };
            var result = await _sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }
    }
}
