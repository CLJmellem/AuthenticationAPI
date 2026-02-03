namespace Auth.Domain.Command.Token
{
    /// <summary>
    /// TokenCommandResponse
    /// </summary>
    public class TokenCommandResponse
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public string Token { get; set; } = string.Empty;

        /// <summary>Gets or sets the expire at.</summary>
        /// <value>The expire at.</value>
        public DateTime? ExpireAt { get; set; }
    }
}