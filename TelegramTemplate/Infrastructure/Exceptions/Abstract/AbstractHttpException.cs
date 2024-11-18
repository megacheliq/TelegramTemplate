using System.Runtime.Serialization;
using TelegramTemplate.Infrastructure.Exceptions.Model;

namespace TelegramTemplate.Infrastructure.Exceptions.Abstract;

public abstract class AbstractHttpException : Exception, IHttpException
{
    protected AbstractHttpException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AbstractHttpException(string message) : base(message)
    {
    }

    protected AbstractHttpException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public abstract int StatusCode { get; }

    public virtual ExceptionMessageDto GetMessage() => new() { Message = Message };
}