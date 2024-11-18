namespace TelegramTemplate.Application.Abstract.UserLogic;

/// <summary>
/// Service of business logic for user
/// </summary>
public interface IUserLogicService
{
    /// <summary>
    /// Ensure that user exists
    /// </summary>
    /// <param name="userId">User's identifier</param>
    /// <param name="username">Username</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task EnsureUserExists(long userId, string? username, CancellationToken cancellationToken);
}