using DataAccess.Domain.Models;
using DataAccess.Repositories.Abstract;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Commands.MessageCommands;

public class CreateMessageCommand : IRequest<Message>
{
    public required long CreatorId { get; set; }
    
    public required int IdInChat { get; set; }
    
    public string? Text { get; set; }
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Message>
{
    private readonly IMessageRepository _messageRepository;

    public CreateMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            CreatorId = request.CreatorId,
            IdInChat = request.IdInChat,
            MessageText = request.Text
        };
        
        return await _messageRepository.AddAsync(message);
    }
}