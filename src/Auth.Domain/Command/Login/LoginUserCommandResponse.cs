namespace Auth.Domain.Command.Login
{
    /// <summary>
    /// LoginUserCommandResponse
    /// </summary>
    public class LoginUserCommandResponse
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public string Token { get; set; } = string.Empty;

        /// <summary>Gets or sets the expiration.</summary>
        /// <value>The expiration.</value>
        public DateTime? Expiration { get; set; }
    }
}