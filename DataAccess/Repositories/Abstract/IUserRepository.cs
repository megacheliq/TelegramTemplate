using DataAccess.Domain.Models;

namespace DataAccess.Repositories.Abstract;

/// <summary>
/// User repository
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Get user by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>User?</returns>
    Task<User?> GetByIdAsync(long id);
    
    /// <summary>
    /// Create user in database
    /// </summary>
    /// <param name="user">User to create</param>
    /// <returns>Created user</returns>
    Task<User> AddAsync(User user);

    /// <summary>
    /// Update user in database
    /// </summary>
    /// <param name="user">User to update</param>
    /// <returns>Updated user</returns>
    Task<User> UpdateAsync(User user);

    /// <summary>
    /// Delete user from database
    /// </summary>
    /// <param name="user">User to delete</param>
    Task DeleteAsync(User user);
}