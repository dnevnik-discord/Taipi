using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.WebSocket;

namespace Taipi.Services;


public class TaipiService : DiscordClientService
{
    public TaipiService(DiscordSocketClient client, ILogger<DiscordClientService> logger) : base(client, logger)
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Client.WaitForReadyAsync(stoppingToken);
        Logger.LogInformation("Taipi is ready!");

        await Client.SetActivityAsync(new Game(name: "bewbs", ActivityType.Watching, flags: ActivityProperties.Join, details: "custom"));

        while (!stoppingToken.IsCancellationRequested)
        {
            Logger.LogInformation("TaipiService running at: {time}", DateTimeOffset.Now);
            await Task.Delay(300000, stoppingToken);
        }
    }
}
