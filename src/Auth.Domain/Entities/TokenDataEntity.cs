using Auth.Domain.Entities.Interface;
using MongoDB.Bson;

namespace Auth.Domain.Entities
{
    /// <summary>
    /// TokenDataEntity
    /// </summary>
    /// <seealso cref="Auth.Domain.Entities.Interface.IBaseEntity" />
    public class TokenDataEntity : IBaseEntity
    {
        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public required ObjectId UserId { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public required string Token { get; set; }

        /// <summary>Gets or sets the expire at.</summary>
        /// <value>The expire at.</value>
        public DateTime ExpireAt { get; set; }
    }
}