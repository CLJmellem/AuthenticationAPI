using Auth.Application.Interfaces;
using Auth.Domain.Command.Token;
using Auth.Domain.Entities;
using Auth.Domain.Exceptions;
using Auth.Domain.Interfaces;
using MediatR;
using MongoDB.Bson;

namespace Auth.Application.Commands.Token
{
    /// <summary>
    /// TokenCommandHandler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;Auth.Domain.Command.Token.TokenCommand, Auth.Domain.Command.Token.TokenCommandResponse&gt;" />
    public class TokenCommandHandler : IRequestHandler<TokenCommand, TokenCommandResponse>
    {
        /// <summary>The token repository.</summary>
        private readonly ITokenRepository _tokenRepository;
        /// <summary>The user repository.</summary>
        private readonly IUserRepository _userRepository;
        /// <summary>The token creation service.</summary>
        private readonly ITokenCreationService _tokenCreationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCommandHandler"/> class.
        /// </summary>
        /// <param name="tokenRepository">The token repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="tokenCreationService">The token creation service.</param>
        public TokenCommandHandler(
            ITokenRepository tokenRepository,
            IUserRepository userRepository,
            ITokenCreationService tokenCreationService)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _tokenCreationService = tokenCreationService;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<TokenCommandResponse> Handle(TokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(ObjectId.Parse(request.UserId));

            if (user == null)
                throw new UserNotFoundException("User not found.");
            else
            {
                (string token, DateTime expiration) = _tokenCreationService.GenerateToken(user.Username, user.Role.ToString());

                await UpdateOrCreateTokenInDatabase(user.Id, token, expiration);

                return new TokenCommandResponse { ExpireAt = expiration, Token = token };
            }
        }

        private async Task UpdateOrCreateTokenInDatabase(ObjectId userId, string token, DateTime expiration)
        {
            var registeredToken = await _tokenRepository.GetOneAsync(t => t.UserId == userId);

            if (registeredToken == null)
            {
                var tokenDataEntity = new TokenDataEntity
                {
                    UserId = userId,
                    Token = token,
                    ExpireAt = expiration,
                };
                await _tokenRepository.CreateAsync(tokenDataEntity);
            }
            else
            {
                registeredToken.ExpireAt = expiration;
                registeredToken.Token = token;

                await _tokenRepository.UpdateAsync(registeredToken);
            }
        }
    }
}