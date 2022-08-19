using Dramatist;

namespace Taipi.Services;


public class DnevnikService : BackgroundService
{
    private readonly ILogger<DnevnikService> _logger;
    private readonly DnevnikClient _dnevnikClient;

    public DnevnikService(HttpClient httpClient, ILogger<DnevnikService> logger)
    {
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        httpClient.DefaultRequestHeaders.UserAgent.Clear();
        httpClient.DefaultRequestHeaders
            .TryAddWithoutValidation("User-Agent", "Unofficial Dnevnik Discord");

        _dnevnikClient = new DnevnikClient(httpClient);
        
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // var get = await _dnevnikClient.GetHttpbinTest("get");
        // _logger.LogInformation(get);

        while (!stoppingToken.IsCancellationRequested)
        {
            // var frontPage = await _dnevnikClient.GetFrontPageAsync();
            // _logger.LogInformation(frontPage);

            var articles = await _dnevnikClient.GetFrontPageArticles(ArticleType.Lead);
            foreach (var article in articles)
            {
                _logger.LogInformation(article.ToString());
            }

            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);
            await Task.Delay(300000, stoppingToken);
        }
    }
}
