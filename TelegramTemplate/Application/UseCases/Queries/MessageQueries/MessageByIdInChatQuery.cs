using DataAccess.Domain.Models;
using DataAccess.Repositories.Abstract;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Queries.MessageQueries;

public class MessageByIdInChatQuery : IRequest<Message?>
{
    public required int IdInChat { get; set; }
}

public class MessageByIdInChatQueryHandler : IRequestHandler<MessageByIdInChatQuery, Message?>
{
    private readonly IMessageRepository _messageRepository;

    public MessageByIdInChatQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Message?> Handle(MessageByIdInChatQuery request, CancellationToken cancellationToken)
    {
        return await _messageRepository.GetMessageByIdInChatAsync(request.IdInChat);
    }
}