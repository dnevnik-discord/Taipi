//using HtmlAgilityPack;

namespace Dramatist;


public class DnevnikClient
{
    private readonly HttpClient _client;

    public DnevnikClient(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri(Dnevnik.BaseAddress);
        _client.DefaultRequestHeaders.Add("Accept", "*/*");
        _client.DefaultRequestHeaders.Add("User-Agent", "Unofficial Dnevnik Discord");
    }

    // ToDo: ensure success, try-catch
    public async Task<string> GetHomepageAsync() => await _client.GetStringAsync("/");

    public async Task<string> GetArticleAsync(int id) => await _client.GetStringAsync($"{id}");

    public async Task<Uri> GetArticleUriAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Head, $"/{id}");
        var response = await _client.SendAsync(request);
        // foreach (var header in response.Headers)
        // {
        //     Console.WriteLine(header.ToString());
        // }
        // var statusCode = (int)response.StatusCode;
        // Console.WriteLine(statusCode);
 
        // throws
        // Console.WriteLine(response.Headers.Location);
        //throw new Exception("Finish me, fucker!");
        return response.Headers.Location;
    }
}
