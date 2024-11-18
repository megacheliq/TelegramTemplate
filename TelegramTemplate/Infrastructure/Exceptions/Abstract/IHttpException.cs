using TelegramTemplate.Infrastructure.Exceptions.Model;

namespace TelegramTemplate.Infrastructure.Exceptions.Abstract;

/// <summary>
/// Interface of exception
/// </summary>
public interface IHttpException
{
    /// <summary>
    /// HTTP statusCode
    /// </summary>
    int StatusCode { get; }

    /// <summary>
    /// Message of the exception
    /// </summary>
    ExceptionMessageDto GetMessage();
}