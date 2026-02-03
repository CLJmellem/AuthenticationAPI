namespace Auth.Application.DTOs.Token
{
    /// <summary>
    /// TokenResponseDTO
    /// </summary>
    public class TokenResponseDTO
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public required string Token { get; set; }

        /// <summary>Gets or sets the expire at.</summary>
        /// <value>The expire at.</value>
        public DateTime? ExpireAt { get; set; }
    }
}