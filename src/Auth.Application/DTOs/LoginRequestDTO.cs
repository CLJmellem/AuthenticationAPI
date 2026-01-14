namespace Auth.Application.DTOs
{
    /// <summary>
    /// The login request dto.
    /// </summary>
    public class LoginRequestDTO
    {
        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        public required string Username { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public required string Password { get; set; }
    }
}
