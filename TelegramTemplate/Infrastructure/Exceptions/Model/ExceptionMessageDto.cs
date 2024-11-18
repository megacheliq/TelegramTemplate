namespace TelegramTemplate.Infrastructure.Exceptions.Model;

/// <summary>
/// Class to form exceptions
/// </summary>
public class ExceptionMessageDto
{
    /// <summary>
    /// Exception message
    /// </summary>
    public string Message { get; set; } = default!;
}