using TelegramTemplate.Infrastructure.Exceptions.Abstract;
using static TelegramTemplate.Infrastructure.Exceptions.Model.ExceptionConstants;

namespace TelegramTemplate.Infrastructure.Exceptions.Model;

public class ForbiddenException : AbstractHttpException
{
    public override int StatusCode => 403;

    public ForbiddenException() : base(FORBIDDEN_MESSAGE.Message)
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}