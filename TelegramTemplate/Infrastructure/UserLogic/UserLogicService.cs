using DataAccess.Domain.Models;
using MediatR;
using TelegramTemplate.Application.Abstract.Storage;
using TelegramTemplate.Application.Abstract.UserLogic;
using TelegramTemplate.Application.Constants;
using TelegramTemplate.Application.UseCases.Commands.UserCommands;
using TelegramTemplate.Application.UseCases.Queries.UserQueries;

namespace TelegramTemplate.Infrastructure.UserLogic;

/// <inheritdoc />
public class UserLogicService : IUserLogicService
{
    private readonly ILogger<UserLogicService> _logger;
    private readonly IStorageService _storageService;
    private readonly IMediator _mediator;

    public UserLogicService(ILogger<UserLogicService> logger,IStorageService storageService, IMediator mediator)
    {
        _logger = logger;
        _storageService = storageService;
        _mediator = mediator;
    }
    
    /// <inheritdoc />
    public async Task EnsureUserExists(long userId, string? username, CancellationToken cancellationToken)
    {
        var keyInCache = $"{StorageConstants.UserInfoPrefix}{userId}";

        // Проверка в кэше
        if (await _storageService.KeyExistsInCacheAsync(keyInCache, cancellationToken))
        {
            await _storageService.ExtendCacheKeyExpirationAsync(keyInCache, cancellationToken);
            return;
        }

        // Поиск в базе данных
        var user = await _mediator.Send(new GetUserByUserIdQuery { UserId = userId }, cancellationToken) 
                   ?? await CreateUserAsync(userId, username, cancellationToken);

        // Добавление в кэш
        await _storageService.CreateObjectInCacheAsync(user, keyInCache, cancellationToken);
    }

    private async Task<User> CreateUserAsync(long userId, string? username, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User {userId} was not found in cache and db, creating...");
        return await _mediator.Send(new CreateUserCommand { UserId = userId, Username = username }, cancellationToken);
    }
}