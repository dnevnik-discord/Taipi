using Dramatist;

namespace Taipi.Services;


public class DnevnikService : BackgroundService
{
    private readonly ILogger<DnevnikService> _logger;
    private readonly DnevnikClient _dnevnikClient;

    public DnevnikService(HttpClient httpClient, ILogger<DnevnikService> logger)
    {
        httpClient.DefaultRequestHeaders.UserAgent.Clear();
        httpClient.DefaultRequestHeaders
            .TryAddWithoutValidation("User-Agent", "Unofficial Dnevnik Discord");

        _dnevnikClient = new DnevnikClient(httpClient);
        
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var get = await _dnevnikClient.GetHttpbinTest("get");
        _logger.LogInformation(get);

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
            var frontPage = await _dnevnikClient.GetFrontPageAsync();
            _logger.LogInformation(frontPage);

            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);
            await Task.Delay(300000, stoppingToken);
        }
    }
}
