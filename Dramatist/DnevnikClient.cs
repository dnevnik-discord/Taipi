using HtmlAgilityPack;

using Dramatist.Models;

namespace Dramatist;


public class DnevnikClient
{
    private readonly HttpClient _client;

    public DnevnikClient(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri(Dnevnik.BaseAddress);
    }

    // ToDo: ensure success, try-catch

    public async Task<string> GetArticleAsync(Uri uri) =>
        await _client.GetStringAsync(uri);

    public async Task<string> GetHttpbinAsync(string endpoint) =>
        await _client.GetStringAsync($"https://httpbin.org/{endpoint}");

    public async Task<List<Article>> GetFrontPageArticlesAsync(ArticleType articleType)
    {
        var response = await _client.GetStringAsync("/");
        var doc = new HtmlDocument();
        doc.LoadHtml(response);

        var nodes = doc.DocumentNode
            .SelectNodes(Dnevnik.XPathFromArticleType(articleType))
            ;

        var articles = new List<Article>();
        // ToDo: NullObject pattern, NullArticle
        // ToDo: return an empty list and handle the error in the caller?
        // ToDo: this should actually return ALL article types
        if (nodes == null)
            articles.Add(new Article("NULL", new Uri(Dnevnik.BaseAddress)));
        else
        {
            foreach (var node in nodes)
            {
                var article = new Article(
                    node.GetAttributeValue("title", "").Replace("&quot;", "\""),
                    new Uri("https://www.dnevnik.bg" + node.GetAttributeValue("href", "")),
                    articleType
                );
                articles.Add(article);
            }
        }
        

        return articles;
    }
}
