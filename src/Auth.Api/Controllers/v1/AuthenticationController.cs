using Auth.Application.DTOs;
using Auth.Domain.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Registers the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            var command = new RegisterUserCommand(
                request.Username,
                request.Email,
                request.Password,
                request.ConfirmPassword
            );

            var responseUserName = await _mediator.Send(command);

            var response = new RegisterResponseDTO() { Message = "User registered successfully" };

            return CreatedAtAction(nameof(Register), new { userName = responseUserName }, response);
        }
    }
}
