namespace DataAccess.Domain.Models;

/// <summary>
/// User entity
/// </summary>
public class User
{
    /// <summary>
    /// User identifier
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Username that displays in telegram
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Date of user creation
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Date of user edit
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}