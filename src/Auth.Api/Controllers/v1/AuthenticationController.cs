using Auth.Application.DTOs.Login;
using Auth.Application.DTOs.Token;
using Auth.Application.DTOs.Register;
using Auth.Application.DTOs.Logout;
using Auth.Domain.Command.Login;
using Auth.Domain.Command.Token;
using Auth.Domain.Command.Logout;
using Auth.Domain.Commands.Register;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.v1
{
    /// <summary>
    /// AuthenticationController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>The mediator</summary>
        private readonly IMediator _mediator;
        /// <summary>The mapper</summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="autoMapper">The automatic mapper.</param>
        public AuthenticationController(
            IMediator mediator,
            IMapper autoMapper)
        {
            _mediator = mediator;
            _mapper = autoMapper;
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

        /// <summary>Logins the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            var command = new LoginUserCommand(
                request.UsernameOrEmail,
                request.Password
            );

            var response =  _mapper.Map<LoginResponseDTO>(await _mediator.Send(command));

            return Ok(response);
        }

        /// <summary>Refreshes the token.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [HttpPost("Token/{userId}")]
        [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenResponseDTO>> TokenRefresh([FromRoute] string userId)
        {
            var command = new TokenCommand( userId );

            var response = _mapper.Map<TokenResponseDTO>(await _mediator.Send(command)); 
            
            return Ok(response);
        }

        /// <summary>Logouts the user.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [HttpDelete("logout/{userId}")]
        [ProducesResponseType(typeof(LogoutResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LogoutResponseDTO>> Logout([FromRoute] string userId)
        {
            var command = new LogoutUserCommand(userId);

            var response = _mapper.Map<LogoutResponseDTO>(await _mediator.Send(command));

            return Ok(response);
        }
    }
}