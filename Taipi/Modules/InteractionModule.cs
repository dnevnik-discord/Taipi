using Discord.Interactions;

namespace Taipi.Modules;

public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("echo", "Echo an input")]
    public async Task Echo(string input)
    {
        await RespondAsync(input);
    }

    [SlashCommand("about", "About Taipi")]
    public async Task About()
    {
        // ToDo: do not hardcode me
        await RespondAsync("https://github.com/dnevnik-discord/Taipi");
    }
}
