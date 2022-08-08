using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Taipi.Services;

// ToDo: FIX ME!
Console.OutputEncoding = System.Text.Encoding.UTF8;

// CreateDefaultBuilder configures a lot of stuff for us automatically
// See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host
var host = Host.CreateDefaultBuilder()
    //.ConfigureLogging( (context, builder) => builder.AddConsole())
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
            .AddHostedService<LongRunningService>()
            // .AddHostedService<Worker>()
            .AddHostedService<DnevnikService>()
            // .AddHttpClient<DnevnikService>(client =>
            //     {
            //         client.BaseAddress =new Uri("https://httpbin.org/");
            //     }
            // )
            .AddHttpClient<DnevnikService>()

            ;

    }).Build();

await host.RunAsync();
