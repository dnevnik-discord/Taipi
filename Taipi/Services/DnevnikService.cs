namespace Taipi.Services;

public class DnevnikService : BackgroundService
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<DnevnikService> _logger;


    public DnevnikService(HttpClient httpClient, ILogger<DnevnikService> logger)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://httpbin.org/");

        _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

        _httpClient.DefaultRequestHeaders.Add("UserAgent", "Unofficial Dnevnik Discord");

        _logger = logger;
    }

    public async Task<string> GetDnevnikAsync() => await _httpClient.GetStringAsync("get");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DnevnikService running at: {time}", DateTimeOffset.Now);

            var response = await GetDnevnikAsync();
            _logger.LogInformation(response.Substring(0, 500));

            await Task.Delay(10000, stoppingToken);
        }
    }
}