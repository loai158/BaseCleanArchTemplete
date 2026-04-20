using Domain.Commands.User;
using Domain.Features.Retailers.Commands;
using Domain.Features.Suppliers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kartona.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register/retailer")]
        public async Task<IActionResult> RegisterRetailer([FromBody] RegisterRetailerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("register/supplier")]
        public async Task<IActionResult> RegisterSupplier([FromBody] RegisterSupplierCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
