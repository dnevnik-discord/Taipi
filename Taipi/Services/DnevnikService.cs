using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.WebSocket;
using HtmlAgilityPack;

namespace Taipi.Services;


public class DnevnikService : DiscordClientService
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<DnevnikService> _logger;

    public DnevnikService(DiscordSocketClient client, HttpClient httpClient, ILogger<DnevnikService> logger) : base(client, logger)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://www.dnevnik.bg");

        _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Unofficial Dnevnik Discord");

        _logger = logger;
    }

    public async Task<string> GetDnevnikAsync() => await _httpClient.GetStringAsync("/");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Client.WaitForReadyAsync(stoppingToken);
        Logger.LogInformation("Client is ready!");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);

            

            var response = await GetDnevnikAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            var article = doc.DocumentNode
                .SelectNodes("/html/body/div[3]/main/div/div[1]/div/div[1]/div/div[1]/article/h3/a")
                .First()
                // .Attributes["href"].Value
                ;

            _logger.LogInformation(article.GetAttributeValue("title", "").Replace("&quot;", "\""));
            _logger.LogInformation("https://www.dnevnik.bg" + article.GetAttributeValue("href", ""));

            await Client.SetActivityAsync(new Game(name: article.GetAttributeValue("title", "").Replace("&quot;", "\""), ActivityType.Watching, flags: ActivityProperties.Join, details: "custom"));

            await Task.Delay(600000, stoppingToken);
        }
    }
}