using Foundatio.Caching;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Queries.StorageQueries;

public class GetObjectFromCacheQuery : IRequest<object>
{
    public required string Key {get; set;}
}

public class GetObjectFromCacheQueryHandler : IRequestHandler<GetObjectFromCacheQuery, object>
{
    private readonly ICacheClient _cache;

    public GetObjectFromCacheQueryHandler(ICacheClient cache)
    {
        _cache = cache;
    }
    
    public async Task<object> Handle(GetObjectFromCacheQuery request, CancellationToken cancellationToken)
    {
        return await _cache.GetAsync(request.Key, cancellationToken);
    }
}