namespace DataAccess.Domain.Models;

/// <summary>
/// Message entity
/// </summary>
public class Message
{
    /// <summary>
    /// Message identifier
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Identifier inside chat
    /// </summary>
    public int IdInChat { get; set; }
    
    /// <summary>
    /// Identifier of creator
    /// </summary>
    public long CreatorId { get; set; }
    
    /// <summary>
    /// Creator of the message
    /// </summary>
    public User Creator { get; set; }
    
    /// <summary>
    /// Content of the message
    /// </summary>
    public string? MessageText { get; set; }
    
    /// <summary>
    /// Is message was deleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;
    
    /// <summary>
    /// Date of message creation
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Date of message edit
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}