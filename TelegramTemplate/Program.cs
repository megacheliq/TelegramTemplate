using System.Text.Json;
using System.Text.Json.Serialization;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.Abstract;
using Foundatio.Caching;
using Foundatio.Serializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Extensions.Hosting;
using StackExchange.Redis;
using Telegram.Bot;
using TelegramTemplate.Application.Abstract.Configuration;
using TelegramTemplate.Application.Abstract.MessageLogic;
using TelegramTemplate.Application.Abstract.Storage;
using TelegramTemplate.Application.Abstract.TelegramBot.Handlers;
using TelegramTemplate.Application.Abstract.TelegramBot.Sending;
using TelegramTemplate.Application.Abstract.TelegramBot.WebHooks;
using TelegramTemplate.Application.Abstract.UserLogic;
using TelegramTemplate.Infrastructure.Configuration;
using TelegramTemplate.Infrastructure.MessageLogic;
using TelegramTemplate.Infrastructure.Middlewares.Extensions;
using TelegramTemplate.Infrastructure.Storage;
using TelegramTemplate.Infrastructure.TelegramBot.Handlers;
using TelegramTemplate.Infrastructure.TelegramBot.Polling;
using TelegramTemplate.Infrastructure.TelegramBot.Sending;
using TelegramTemplate.Infrastructure.TelegramBot.WebHooks;
using TelegramTemplate.Infrastructure.TelegramBot.WebHooks.Extensions;
using TelegramTemplate.Infrastructure.UserLogic;
using TelegramTemplate.Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("Default");
var redisConnectionString = configuration.GetConnectionString("Redis") ??
                            throw new NullReferenceException("Redis connection string is null");

var logger = LogManager.GetCurrentClassLogger();
var isUsedWebHook = false;

try
{
    builder.Services.AddHealthChecks();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog(new NLog.Extensions.Logging.NLogProviderOptions { RemoveLoggerFactoryFilter = false });

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
    
    builder.Services.AddSingleton<ICacheClient>(new RedisHybridCacheClient(new RedisHybridCacheClientOptions
    {
        ConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString),
        Serializer = new SystemTextJsonSerializer(),
        ShouldThrowOnSerializationError = true
    }));
    
    builder.Services.Configure<BotConfiguration>(builder.Configuration.GetSection(BotConfiguration.Configuration));
    builder.Services.AddSingleton<IBotConfiguration>(sp =>
    {
        var botConfig = sp.GetRequiredService<IOptions<BotConfiguration>>().Value;
        return botConfig;
    });

    builder.Services.Configure<ClientAppConfiguration>(
        builder.Configuration.GetSection(ClientAppConfiguration.Configuration));
    builder.Services.AddSingleton<IClientAppConfiguration>(sp =>
    {
        var clientConfig = sp.GetRequiredService<IOptions<ClientAppConfiguration>>().Value;
        return clientConfig;
    });
    
    builder.Services.AddHttpClient("telegram_bot_client")
        .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
        {
            var botConfiguration = sp.GetRequiredService<IBotConfiguration>();

            ArgumentNullException.ThrowIfNull(botConfiguration);

            TelegramBotClientOptions options = new(botConfiguration.BotToken);
            return new TelegramBotClient(options, httpClient);
        });
    
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
    
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IMessageRepository, MessageRepository>();

    builder.Services.AddScoped<IStorageService, StorageService>();
    builder.Services.AddScoped<IUserLogicService, UserLogicService>();
    builder.Services.AddScoped<IMessageLogicService, MessageLogicService>();
    builder.Services.AddScoped<ITelegramSendingService, TelegramSendingService>();
    
    builder.Services.AddScoped<IMessageHandler, MessageHandler>();
    
    builder.Services.AddScoped<ISwapTechnology, SwapTechnology>();
    builder.Services.AddScoped<UpdateHandler>();
    
    if (isUsedWebHook)
    {
        builder.Services.AddHostedService<ConfigureWebHook>();
    }
    else
    {
        builder.Services.AddScoped<ReceiverService>();
        builder.Services.AddHostedService<PollingService>();
    }

    builder.Services.AddControllers()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

    builder.Services.AddSwaggerGen(opt =>
    {
        var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        opt.CustomSchemaIds(type => type.FullName);

        if (File.Exists(xmlPath))
        {
            opt.IncludeXmlComments(xmlPath);
        }
        else
        {
            logger.Warn($"XML documentation file '{xmlFile}' not found.");
        }
    });
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            policy =>
            {
                var clientAppConfig = builder.Configuration.GetSection("ClientApp").Get<ClientAppConfiguration>();
                policy.WithOrigins(clientAppConfig?.Address ??
                                   throw new InvalidOperationException("ClientApp Address is not configured"))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
    });

    var app = builder.Build();

    app.UseConfigPathBase(configuration);
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowSpecificOrigin");

    app.UseRouting();
    app.UseJsonException();
    app.UseAuthentication();
    app.UseAuthorization();
    
    var botConfiguration = app.Services.GetRequiredService<IBotConfiguration>();
    app.MapBotWebhookRoute<WebHookController>(route: botConfiguration.Route);
    app.MapControllers();
    
    app.MapHealthChecks("/healthz");

    await app.RunAsync();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}