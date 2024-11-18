using Foundatio.Caching;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Commands.StorageCommands;

public class ExtendCacheKeyExpirationCommand : IRequest
{
    public required string Key { get; set; }
}

public class ExtendCacheKeyExpirationCommandHandler : IRequestHandler<ExtendCacheKeyExpirationCommand>
{
    private readonly ICacheClient _cache;

    public ExtendCacheKeyExpirationCommandHandler(ICacheClient cache)
    {
        _cache = cache;
    }

    public async Task Handle(ExtendCacheKeyExpirationCommand request, CancellationToken cancellationToken)
    {
        await _cache.SetExpirationAsync(request.Key, TimeSpan.FromHours(1));
    }
}