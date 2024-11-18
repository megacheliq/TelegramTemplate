using System.Runtime.Serialization;
using TelegramTemplate.Infrastructure.Exceptions.Abstract;
using static TelegramTemplate.Infrastructure.Exceptions.Model.ExceptionConstants;

namespace TelegramTemplate.Infrastructure.Exceptions.Model;

public class NoDataFoundException : AbstractHttpException
{
    public override int StatusCode => 404;

    public NoDataFoundException() : base(NOT_FOUND_MESSAGE.Message)
    {
    }

    public NoDataFoundException(string message) : base(message)
    {
    }

    protected NoDataFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoDataFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}