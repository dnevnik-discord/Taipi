using Discord.Interactions;

namespace Taipi.Modules;

public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("echo", "Echo an input")]
    public async Task Echo(string input)
    {
        await RespondAsync(input);
    }
}