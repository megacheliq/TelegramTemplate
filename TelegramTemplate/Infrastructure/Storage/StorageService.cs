using MediatR;
using TelegramTemplate.Application.Abstract.Storage;
using TelegramTemplate.Application.UseCases.Commands.StorageCommands;
using TelegramTemplate.Application.UseCases.Queries.StorageQueries;

namespace TelegramTemplate.Infrastructure.Storage;

/// <inheritdoc />
public class StorageService : IStorageService
{
    private readonly ILogger<StorageService> _logger;
    private readonly IMediator _mediator;

    public StorageService(ILogger<StorageService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    /// <inheritdoc />
    public async Task CreateObjectInCacheAsync(object objectToSave, string key, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Saving object in cache for {key}");
        await _mediator.Send(new CreateObjectInCacheCommand { Object = objectToSave, Key = key },
            cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> KeyExistsInCacheAsync(string key, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Checking if key {key} exists in cache");
        return await _mediator.Send(new DoesKeyExistsInCacheQuery { Key = key }, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task ExtendCacheKeyExpirationAsync(string key, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Extending cache key {key}");
        await _mediator.Send(new ExtendCacheKeyExpirationCommand { Key = key }, cancellationToken);
    }
}