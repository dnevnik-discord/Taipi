using HtmlAgilityPack;

using Dramatist;


namespace Taipi.Services;


public class DnevnikService : BackgroundService
{
    private readonly DnevnikClient _dnevnikClient;
    private readonly ILogger<DnevnikService> _logger;

    public DnevnikService(ILogger<DnevnikService> logger)
    {
        _logger = logger;
        //_dnevnikClient = new DnevnikClient(new HttpClient());
        HttpClientHandler handler = new HttpClientHandler();
        //handler.AllowAutoRedirect = false;
        handler.MaxAutomaticRedirections = 1;
        _dnevnikClient = new DnevnikClient(new HttpClient(handler));
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // ToDo: (http)clienr ready?
        
        int articleId = 4379799;
        var uri = await _dnevnikClient.GetArticleUriAsync(articleId);
        _logger.LogInformation($"Full URI for article id {articleId}: {uri}");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);

            var response = await _dnevnikClient.GetHomepageAsync();
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