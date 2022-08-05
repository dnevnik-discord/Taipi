using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Taipi;

// CreateDefaultBuilder configures a lot of stuff for us automatically
// See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host
var host = Host.CreateDefaultBuilder()   
    .ConfigureDiscordHost((context, config) =>
    {
        config.SocketConfig = new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Debug,
            AlwaysDownloadUsers = true,
            MessageCacheSize = 200
        };

        config.Token = context.Configuration["Token"];
    })
    // Optionally wire up the command service
    .UseCommandService((context, config) =>
    {
        config.DefaultRunMode = RunMode.Async;
        config.CaseSensitiveCommands = false;
        config.LogLevel = LogSeverity.Debug;
    })
    // Optionally wire up the interactions service
    .UseInteractionService((context, config) =>
    {
        config.LogLevel = LogSeverity.Debug;
        config.UseCompiledLambda = true;
    })
    .ConfigureServices((context, services) =>
    {
        //Add any other services here
        services
            .AddHostedService<CommandHandler>()
            .AddHostedService<InteractionHandler>()
            .AddHostedService<BotStatusService>()
            .AddHostedService<LongRunningService>();


    }).Build();
  
await host.RunAsync();
