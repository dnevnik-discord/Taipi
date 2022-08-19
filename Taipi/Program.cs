using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Taipi.Services;

// ToDo: FIX ME!
Console.OutputEncoding = System.Text.Encoding.UTF8;

// CreateDefaultBuilder configures a lot of stuff for us automatically
// See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host
var host = Host.CreateDefaultBuilder()
    .ConfigureDiscordHost((context, config) =>
    {
        config.SocketConfig = new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Debug,
            AlwaysDownloadUsers = true,
            MessageCacheSize = 200,
            GatewayIntents = GatewayIntents.All
        };

        config.Token = context.Configuration["Token"];
    })
    .UseCommandService((context, config) =>
    {
        config.DefaultRunMode = RunMode.Async;
        config.CaseSensitiveCommands = false;
        config.LogLevel = LogSeverity.Debug;
    })
    .UseInteractionService((context, config) =>
    {
        config.LogLevel = LogSeverity.Debug;
        config.UseCompiledLambda = true;
    })
    .ConfigureServices((context, services) =>
    {
        services
            .AddHostedService<CommandHandler>()
            .AddHostedService<InteractionHandler>()
            .AddHostedService<TaipiService>()
            .AddHostedService<DnevnikService>()
            .AddHttpClient<DnevnikService>()
            ;

    }).Build();

await host.RunAsync();
