using Foundatio.Caching;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Queries.StorageQueries;

public class DoesKeyExistsInCacheQuery : IRequest<bool>
{
    public required string Key { get; set; }
}

public class DoesKeyExistsInCacheQueryHandler : IRequestHandler<DoesKeyExistsInCacheQuery, bool>
{
    private readonly ICacheClient _cache;

    public DoesKeyExistsInCacheQueryHandler(ICacheClient cache)
    {
        _cache = cache;
    }

    public async Task<bool> Handle(DoesKeyExistsInCacheQuery request, CancellationToken cancellationToken)
    {
        return await _cache.ExistsAsync(request.Key);
    }
}