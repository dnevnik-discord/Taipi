using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Taipi.Modules;

public class PublicModule : ModuleBase<SocketCommandContext>
{
    private readonly ILogger<PublicModule> _logger;
    //You can inject the host. This is useful if you want to shutdown the host via a command, but be careful with it.
    private readonly IHost _host;

    public PublicModule(IHost host, ILogger<PublicModule> logger)
    {
        _host = host;
        _logger = logger;
    }

    [Command("ping")]
    [Alias("pong", "hello")]
    public async Task PingAsync()
    {
        _logger.LogInformation("User {user} used the ping command!", Context.User.Username);
        await ReplyAsync("pong!");
    }

    [Command("shutdown")]
    [RequireUserPermission(GuildPermission.Administrator)]
    public Task Stop()
    {
        _ = _host.StopAsync();
        return Task.CompletedTask;
    }

    [Command("log")]
    public Task TestLogs()
    {
        _logger.LogDebug("НТЖТЯ ТЖАТГУ");
        _logger.LogTrace("This is a trace log");
        _logger.LogDebug("This is a debug log");
        _logger.LogInformation("This is an information log");
        _logger.LogWarning("This is a warning log");
        _logger.LogError(new InvalidOperationException("Invalid Operation"), "This is a error log with exception");
        _logger.LogCritical(new InvalidOperationException("Invalid Operation"), "This is a critical load with exception");

        _logger.Log(GetLogLevel(LogSeverity.Error), "Error logged from a Discord LogSeverity.Error");
        _logger.Log(GetLogLevel(LogSeverity.Info), "Information logged from Discord LogSeverity.Info ");

        return Task.CompletedTask;
    }

    [Command("info")]
    public async Task InfoAsync(SocketGuildUser socketGuildUser = null)
    {
        if (socketGuildUser == null)
            socketGuildUser = Context.User as SocketGuildUser;

        await ReplyAsync($"ID: {socketGuildUser.Id}\n" +
            $"Name: {socketGuildUser.Username}#{socketGuildUser.Discriminator}\n" +
            $"Created at: {socketGuildUser.CreatedAt}\n" +
            $"Joined at: {socketGuildUser.JoinedAt}\n" +
            $"Status: {socketGuildUser.Status}\n" +
            $"Active clients: {socketGuildUser.ActiveClients}\n" +
            $"Activities: {socketGuildUser.Activities}\n" +
            $"Hash code: {socketGuildUser.GetHashCode()}\n" +
            $"Guild permissions: {socketGuildUser.GuildPermissions}\n" +
            $"Hierarchy: {socketGuildUser.Hierarchy}\n" +
            $"Public flags: {socketGuildUser.PublicFlags}\n" +
            $"Roles: {socketGuildUser.Roles}\n"
            );
    }

    private static LogLevel GetLogLevel(LogSeverity severity)
        => (LogLevel)Math.Abs((int)severity - 5);
}
