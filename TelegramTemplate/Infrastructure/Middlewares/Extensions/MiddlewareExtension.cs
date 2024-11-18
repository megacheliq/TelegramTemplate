namespace TelegramTemplate.Infrastructure.Middlewares.Extensions;

public static class MiddlewareExtensions
{
    /// <summary>
    /// Integration <see cref="ExceptionMidleware"/>
    /// </summary>
    /// <param name="app">App</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void UseJsonException(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseMiddleware<ExceptionMidleware>();
    }

    /// <summary>
    /// Base route
    /// </summary>
    /// <param name="app">App</param>
    /// <param name="configuration">Configuration</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void UseConfigPathBase(this IApplicationBuilder app, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(app);

        ArgumentNullException.ThrowIfNull(configuration);

        app.UsePathBase(new PathString(configuration["AppName"]));
    }
}