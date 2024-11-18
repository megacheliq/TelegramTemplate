using DataAccess.Domain.Models;

namespace DataAccess.Repositories.Abstract;

/// <summary>
/// Message repository
/// </summary>
public interface IMessageRepository
{
    /// <summary>
    /// Get message by identifier in chat
    /// </summary>
    /// <param name="idInChat">Identifier in chat</param>
    /// <returns>Message?</returns>
    Task<Message?> GetMessageByIdInChatAsync(int idInChat);
    
    /// <summary>
    /// Get message by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Message?</returns>
    Task<Message?> GetByIdAsync(long id);
    
    /// <summary>
    /// Create message in database
    /// </summary>
    /// <param name="message">Message to create</param>
    /// <returns>Created message</returns>
    Task<Message> AddAsync(Message message);

    /// <summary>
    /// Update message in database
    /// </summary>
    /// <param name="message">Message to update</param>
    /// <returns>Updated message</returns>
    Task<Message> UpdateAsync(Message message);

    /// <summary>
    /// Delete message from database
    /// </summary>
    /// <param name="message">Message to delete</param>
    Task DeleteAsync(Message message);
}