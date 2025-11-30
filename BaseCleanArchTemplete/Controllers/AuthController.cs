using Domain.Commands.User;
using Domain.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseCleanArchTemplete.Controllers
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
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RigisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
