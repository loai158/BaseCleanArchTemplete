using Domain.Features.Categories.Commands;
using Domain.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kartona.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetCategoriesQuery query)
        {
            var result = await _mediator.Send(query);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery { Id = id });
            if (!result.IsSuccess)
                return NotFound(result);
            return Ok(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand { Id = id });
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
