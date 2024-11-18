using Foundatio.Caching;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Commands.StorageCommands;

public class CreateObjectInCacheCommand : IRequest
{
    public required object Object { get; set; }
    
    public required string Key { get; set; }
}

public class CreateObjectInCacheCommandHandler : IRequestHandler<CreateObjectInCacheCommand>
{
    private readonly ICacheClient _cache;

    public CreateObjectInCacheCommandHandler(ICacheClient cache)
    {
        _cache = cache;
    }

    public async Task Handle(CreateObjectInCacheCommand request, CancellationToken cancellationToken)
    {
        await _cache.SetAsync(request.Key, request.Object, DateTime.UtcNow.AddHours(1)); // ttl - 1 hour
    }
}