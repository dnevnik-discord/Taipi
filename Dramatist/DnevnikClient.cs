//using HtmlAgilityPack;

namespace Dramatist;


public class DnevnikClient
{
    private readonly HttpClient _client;

    public DnevnikClient(HttpClient client)
    {
        // ToDo: How to disable redirects here?
        // HttpClientHandler handler = new HttpClientHandler();
        // handler.MaxAutomaticRedirections = 1;
        _client = client;
        _client.BaseAddress = new Uri(Dnevnik.BaseAddress);
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Add("Accept", "*/*");
        _client.DefaultRequestHeaders.UserAgent.Clear();
        _client.DefaultRequestHeaders.Add("User-Agent", "Unofficial Dnevnik Discord");
    }

    // ToDo: ensure success, try-catch
    public async Task<string> GetHomepageAsync() => await _client.GetStringAsync("/");

    public async Task<string> GetArticleAsync(int id) => await _client.GetStringAsync($"{id}");

    public async Task<Uri> GetArticleUriAsync(int id)
    {
        // ToDo: How to disable redirects here?
        var request = new HttpRequestMessage(HttpMethod.Head, $"{id}");
        var response = await _client.SendAsync(request);
        return response.Headers.Location;
    }
}
