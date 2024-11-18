using DataAccess.Domain.Models;
using MediatR;
using TelegramTemplate.Application.Abstract.MessageLogic;
using TelegramTemplate.Application.UseCases.Commands.MessageCommands;
using TelegramTemplate.Application.UseCases.Queries.MessageQueries;
using TelegramTemplate.Infrastructure.Exceptions.Model;

namespace TelegramTemplate.Infrastructure.MessageLogic;

/// <inheritdoc />
public class MessageLogicService : IMessageLogicService
{
    private readonly ILogger<MessageLogicService> _logger;
    private readonly IMediator _mediator;

    public MessageLogicService(ILogger<MessageLogicService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    /// <inheritdoc />
    public async Task CreateMessageAsync(long creatorId, int idInChat, string? text, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Creating message: {creatorId}, text: {text}");
        await _mediator.Send(new CreateMessageCommand { CreatorId = creatorId, IdInChat = idInChat, Text = text }, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateMessageAsync(int idInChat, string? text, CancellationToken cancellationToken)
    {
        var message = await MessageByIdInChatAsync(idInChat, cancellationToken);

        if (message == null)
        {
            throw new NoDataFoundException("Message not found");
        }
        
        _logger.LogDebug($"Updating message: {idInChat}, text: {text}");
        await _mediator.Send(new UpdateMessageCommand { MessageToUpdate = message, Text = text }, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<Message?> MessageByIdInChatAsync(int idInChat, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"MessageByIdInChatAsync: {idInChat}");
        return await _mediator.Send(new MessageByIdInChatQuery { IdInChat = idInChat }, cancellationToken);
    }
}