using DataAccess.Domain.Models;

namespace TelegramTemplate.Application.Abstract.MessageLogic;

/// <summary>
/// Service of business logic for messages
/// </summary>
public interface IMessageLogicService
{
    /// <summary>
    /// Create message
    /// </summary>
    /// <param name="creatorId">Creator identifier</param>
    /// <param name="text">Content of the message</param>
    /// <param name="idInChat">Identifier inside chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task CreateMessageAsync(long creatorId, int idInChat, string? text, CancellationToken cancellationToken);

    /// <summary>
    /// Update message
    /// </summary>
    /// <param name="text">Content of the message</param>
    /// <param name="idInChat">Identifier inside chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateMessageAsync(int idInChat, string? text, CancellationToken cancellationToken);

    /// <summary>
    /// Get message by identifier in chat
    /// </summary>
    /// <param name="idInChat">Identifier inside chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Message?</returns>
    Task<Message?> MessageByIdInChatAsync(int idInChat, CancellationToken cancellationToken);
}