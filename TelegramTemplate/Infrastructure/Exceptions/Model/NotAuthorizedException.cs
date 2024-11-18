using TelegramTemplate.Infrastructure.Exceptions.Abstract;
using static TelegramTemplate.Infrastructure.Exceptions.Model.ExceptionConstants;

namespace TelegramTemplate.Infrastructure.Exceptions.Model;

public class NotAuthorizedException : AbstractHttpException
{
    public override int StatusCode => 401;

    public NotAuthorizedException() : base(NOT_AUTHORIZED_MESSAGE.Message)
    {
    }

    public NotAuthorizedException(string message) : base(message)
    {
    }

    public NotAuthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}