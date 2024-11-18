using TelegramTemplate.Infrastructure.Exceptions.Abstract;

namespace TelegramTemplate.Infrastructure.Exceptions.Model;

public class BusinessLogicException : AbstractHttpException
{
    public override int StatusCode => 500;

    public BusinessLogicException(string message) : base(message)
    {

    }

    public BusinessLogicException(string message, Exception innerException) : base(message, innerException)
    {
    }
}