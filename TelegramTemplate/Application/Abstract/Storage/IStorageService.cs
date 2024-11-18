namespace TelegramTemplate.Application.Abstract.Storage;

/// <summary>
/// Service for cache
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Create object in cache
    /// </summary>
    /// <param name="objectToSave">Object</param>
    /// <param name="key">Key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task CreateObjectInCacheAsync(object objectToSave, string key, CancellationToken cancellationToken);
    
    /// <summary>
    /// Check if key exists in cache
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>boolean</returns>
    Task<bool> KeyExistsInCacheAsync(string key, CancellationToken cancellationToken);

    /// <summary>
    /// Extend key ttl
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task ExtendCacheKeyExpirationAsync(string key, CancellationToken cancellationToken);
}