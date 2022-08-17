using HtmlAgilityPack;
using System.Net.Http.Headers;

using Dramatist.Models;

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
        //_client.DefaultRequestHeaders.Add("Accept", "*/*");
        _client.DefaultRequestHeaders.Accept.Add(
            (new MediaTypeWithQualityHeaderValue("*/*"))
        );
        _client.DefaultRequestHeaders.UserAgent.Clear();
        //_client.DefaultRequestHeaders.Add("User-Agent", "Unofficial Dnevnik Discord");
        // _client.DefaultRequestHeaders.UserAgent.Add(
        //     (ProductInfoHeaderValue.Parse("Unofficial Dnevnik Discord"))
        // );
        _client.DefaultRequestHeaders.TryAddWithoutValidation(
            "User-Agent", 
            "Unofficial Dnevnik Discord"
        );
    }

    // ToDo: ensure success, try-catch
    public async Task<string> GetHomepageAsyncBack() =>
        await _client.GetStringAsync("/");

    public async Task<string> GetFrontPageAsync()
    {
        var response = await _client.GetStringAsync("/");
        var doc = new HtmlDocument();
        doc.LoadHtml(response);
        var node = doc.DocumentNode
            .SelectNodes(Dnevnik.XPathFromArticleType(ArticleType.Lead))
            .First()
            // .Attributes["href"].Value
            ;

        var article = new Article(
            node.GetAttributeValue("title", "").Replace("&quot;", "\""),
            new Uri("https://www.dnevnik.bg" + node.GetAttributeValue("href", ""))
        );

        return $"{article.Title}: {article.Uri}";
    }

    public async Task<string> GetArticleAsync(int id) =>
        await _client.GetStringAsync($"{id}");

    public async Task<Uri> GetArticleUriAsync(int id)
    {
        // ToDo: How to disable redirects here?
        var request = new HttpRequestMessage(HttpMethod.Head, $"{id}");
        var response = await _client.SendAsync(request);
        return response.Headers.Location;
    }

    public async Task<string> GetHttpbinTest(string endpoint) =>
        await _client.GetStringAsync($"https://httpbin.org/{endpoint}");

}
