using Dramatist;

namespace Taipi.Services;


public class DnevnikService : BackgroundService
{
    private readonly DnevnikClient _dnevnikClient;
    private readonly ILogger<DnevnikService> _logger;

    public DnevnikService(HttpClient httpClient, ILogger<DnevnikService> logger)
    {
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        httpClient.DefaultRequestHeaders.UserAgent.Clear();
        httpClient.DefaultRequestHeaders
            .TryAddWithoutValidation("User-Agent", "Unofficial Dnevnik Discord; https://discord.gg/DYw5UNqksB");

        _dnevnikClient = new DnevnikClient(httpClient);
        
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var get = await _dnevnikClient.GetHttpbinAsync("get");
        _logger.LogInformation(get);

        while (!stoppingToken.IsCancellationRequested)
        {
            var articles = await _dnevnikClient.GetFrontPageArticlesAsync(ArticleType.Important);
            foreach (var article in articles)
            {
                _logger.LogInformation(article.ToString());
            }

            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);
            await Task.Delay(300000, stoppingToken);
        }
    }
}
