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

    public async Task<string> GetFrontPageAsync()
    {
        var response = await _client.GetStringAsync("/");
        var doc = new HtmlDocument();
        doc.LoadHtml(response);
        var articleType = ArticleType.Lead;
        var node = doc.DocumentNode
            .SelectNodes(Dnevnik.XPathFromArticleType(articleType))
            .First()
            // .Attributes["href"].Value
            ;

        var article = new Article(
            node.GetAttributeValue("title", "").Replace("&quot;", "\""),
            new Uri("https://www.dnevnik.bg" + node.GetAttributeValue("href", "")),
            articleType
        );

        return article.ToString();
////////////////////////////////////////////////////////////////////
        // var articleType = ArticleType.Lead;

        // var articles = GetFrontPageArticles(articleType, doc);
        // var str = "";
        // foreach (var article in articles)
        // {
        //     str += $"{article.ToString()}\n";
        // }
        
        // return str;
    }

    public async Task<string> GetArticleAsync(Uri uri) =>
        await _client.GetStringAsync(uri);

    public async Task<string> GetHttpbinTest(string endpoint) =>
        await _client.GetStringAsync($"https://httpbin.org/{endpoint}");

    public async Task<List<Article>> GetFrontPageArticles(ArticleType articleType)
    {
        var response = await _client.GetStringAsync("/");
        var doc = new HtmlDocument();
        doc.LoadHtml(response);
        
        var nodes = doc.DocumentNode
            .SelectNodes(Dnevnik.XPathFromArticleType(articleType))
            ;

        var articles = new List<Article>() ;
        foreach (var node in nodes)
        {
            var article = new Article(
                node.GetAttributeValue("title", "").Replace("&quot;", "\""),
                new Uri("https://www.dnevnik.bg" + node.GetAttributeValue("href", "")),
                articleType
            );
            articles.Add(article);
        }
        return articles;
    }

}
