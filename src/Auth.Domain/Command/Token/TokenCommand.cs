using MediatR;

namespace Auth.Domain.Command.Token
{
    /// <summary>
    /// TokenCommand
    /// </summary>
    /// <seealso cref="MediatR.IRequest&lt;System.String&gt;" />
    /// <seealso cref="MediatR.IBaseRequest" />
    /// <seealso cref="System.IEquatable&lt;Auth.Domain.Command.Token.TokenCommand&gt;" />
    public record TokenCommand(
        string UserId
        ) : IRequest<TokenCommandResponse>;
}