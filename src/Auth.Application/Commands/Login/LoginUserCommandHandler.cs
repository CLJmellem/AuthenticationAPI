using Auth.Application.DTOs.Login;
using Auth.Application.Interfaces;
using Auth.Domain.Command.Login;
using Auth.Domain.Entities;
using Auth.Domain.Exceptions;
using Auth.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Commands.Login
{
    /// <summary>
    /// LoginUserCommandHandler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;Auth.Domain.Command.Login.LoginUserCommand, Auth.Domain.Command.Login.LoginUserCommandResponse&gt;" />
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        /// <summary>The user repository</summary>
        private readonly IUserRepository _userRepository;
        /// <summary>The token service</summary>
        private readonly ITokenCreationService _tokenService;
        /// <summary>The configuration</summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="tokenCreationService">The token creation service.</param>
        /// <param name="configuration">The configuration.</param>
        public LoginUserCommandHandler(
                    IUserRepository userRepository,
                    ITokenCreationService tokenCreationService,
                    IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenCreationService;
            _configuration = configuration;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        /// <exception cref="UserNotFoundException">User not found.</exception>
        /// <exception cref="InvalidUserDataException">Invalid password.</exception>
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetOneAsync(ur => ur.Email == request.UsernameOrEmail || ur.Username == request.UsernameOrEmail);

            // In a real world application, avoid revealing whether the user exists or not. It should be a generic message for both cases.
            if (user == null)
                throw new UserNotFoundException("User not found.");
            else
            {
                if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user?.PasswordHash))
                    throw new InvalidUserDataException("Invalid password.");
                
                (string token, DateTime expiration) = _tokenService.GenerateToken(user.Username, user.Role.ToString());

                return new LoginUserCommandResponse() 
                { 
                    Token = token,
                    Expiration = expiration
                };
            }
        }
    }
}