using HtmlAgilityPack;

using Dramatist;


namespace Taipi.Services;


public class DnevnikService : BackgroundService
{
    private readonly ILogger<DnevnikService> _logger;
    private readonly DnevnikClient _dnevnikClient;

    public DnevnikService(HttpClient httpClient, ILogger<DnevnikService> logger)
    {
        _logger = logger;
        _dnevnikClient = new DnevnikClient(httpClient);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // ToDo: (http)clienr ready?

        var httpbin = await _dnevnikClient.GetHttpbinTest("get");
        _logger.LogInformation(httpbin);

        int articleId = 4379799;
        // ToDo: try-catch, status checks in DnevnikClient
        try
        {
            var uri = await _dnevnikClient.GetArticleUriAsync(articleId);
            _logger.LogInformation($"Full URI for article id {articleId}: {uri}");
        }
        catch (System.Exception)
        {
            //throw;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);

            var frontPage = await _dnevnikClient.GetFrontPageAsync();
            _logger.LogInformation(frontPage);


            await Task.Delay(300000, stoppingToken);
        }
    }
}
