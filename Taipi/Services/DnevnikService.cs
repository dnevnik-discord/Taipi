using HtmlAgilityPack;

namespace Taipi.Services;

public class DnevnikService : BackgroundService
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<DnevnikService> _logger;


    public DnevnikService(HttpClient httpClient, ILogger<DnevnikService> logger) : base()
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://www.dnevnik.bg/");

        _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

        _httpClient.DefaultRequestHeaders.Add("UserAgent", "Unofficial Dnevnik Discord");

        _logger = logger;
    }

    public async Task<string> GetDnevnikAsync() => await _httpClient.GetStringAsync("");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {


        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);

            var response = await GetDnevnikAsync();
            //_logger.LogInformation(response.Substring(0, 500));
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            var article = doc.DocumentNode
                .SelectNodes("/html/body/div[3]/main/div/div[1]/div/div[1]/div/div[1]/article/h3/a")
                .First()
                // .Attributes["href"].Value
                ;

            _logger.LogInformation(article.GetAttributeValue("title", "").Replace("&quot;", "\""));
            _logger.LogInformation("https://www.dnevnik.bg" + article.GetAttributeValue("href", ""));

            await Task.Delay(600000, stoppingToken);
        }
    }
}