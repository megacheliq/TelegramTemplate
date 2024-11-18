namespace TelegramTemplate.Infrastructure.TelegramBot.WebHooks.Extensions;

/// <summary>
/// Extensions for webhooks
/// </summary>
public static class WebHookExtensions
{
    /// <summary>
    /// Map the hooks
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="endpoints">Endpoints</param>
    /// <param name="route">Route</param>
    public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<T>(
        this IEndpointRouteBuilder endpoints,
        string route)
    {
        var controllerName = typeof(T).Name.Replace("Controller", "", StringComparison.Ordinal);
        var actionName = typeof(T).GetMethods()[0].Name;

        return endpoints.MapControllerRoute(
            name: "bot_webhook",
            pattern: route,
            defaults: new { controller = controllerName, action = actionName });
    }
}