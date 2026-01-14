using Auth.Domain.Entities;

namespace Auth.Domain.Interfaces;

/// <summary>
/// IUserRepository
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>Gets the by email asynchronous.</summary>
    /// <param name="email">The email.</param>
    /// <returns></returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>Gets the by username asynchronous.</summary>
    /// <param name="username">The username.</param>
    /// <returns></returns>
    Task<User?> GetByUsernameAsync(string username);
}