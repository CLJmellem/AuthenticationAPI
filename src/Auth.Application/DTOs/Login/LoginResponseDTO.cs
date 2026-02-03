namespace Auth.Application.DTOs.Login
{
    /// <summary>
    /// The login response dto.
    /// </summary>
    public class LoginResponseDTO
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public string? Token { get; set; }

        /// <summary>Gets or sets the expiration.</summary>
        /// <value>The expiration.</value>
        public DateTime? Expiration { get; set; }
    }
}