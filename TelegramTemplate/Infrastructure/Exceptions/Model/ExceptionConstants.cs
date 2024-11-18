namespace TelegramTemplate.Infrastructure.Exceptions.Model;

/// <summary>
/// Class that have constants of the exceptions 
/// </summary>
public static class ExceptionConstants
{
    public static readonly ExceptionMessageDto FORBIDDEN_MESSAGE = new()
        { Message = "You are not allowed to use this command" };

    public static readonly ExceptionMessageDto NOT_FOUND_MESSAGE = new()
        { Message = "Data you are trying to access does not exist" };

    public static readonly ExceptionMessageDto NOT_AUTHORIZED_MESSAGE = new()
        { Message = "You are not authorized to access this command" };
    
    public static ExceptionMessageDto GetMethodNotAllowedMessage(string methodName)
        => new() { Message = $"Method {methodName} does not exists in this controller" };
}