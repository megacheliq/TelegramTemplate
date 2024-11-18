using DataAccess.Domain.Models;
using DataAccess.Repositories.Abstract;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Commands.MessageCommands;

public class UpdateMessageCommand : IRequest<Message>
{
    public required Message MessageToUpdate { get; set; }
    public string? Text { get; set; }
}

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Message>
{
    private readonly IMessageRepository _messageRepository;

    public UpdateMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Message> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        request.MessageToUpdate.MessageText = request.Text;
        return await _messageRepository.UpdateAsync(request.MessageToUpdate);
    }
}