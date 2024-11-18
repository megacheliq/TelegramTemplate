using TelegramTemplate.Infrastructure.Exceptions.Abstract;

namespace TelegramTemplate.Infrastructure.Exceptions.Model;

public class BadRequestException : AbstractHttpException
{

    public override int StatusCode => 400;

    public BadRequestException(string message) : base(message)
    {

    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}