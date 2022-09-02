using HtmlAgilityPack;

using Dramatist.Models;

namespace Dramatist;


public class DnevnikClient
{
    private readonly HttpClient _client;

    private static List<Article> FrontPageArticles;

    public DnevnikClient(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri(Dnevnik.BaseAddress);
    }

    // ToDo: response.Headers.Location

    // ToDo: use 'private async Task<string> GetStringAsync()'
    public async Task<string> GetArticleAsync(Uri uri) =>
        await _client.GetStringAsync(uri);

    // ToDo: arguments/parameters or delete me
    public async Task<string> GetHttpbinAsync(string endpoint) =>
        await GetStringAsync($"https://httpbin.org/{endpoint}");

    public async Task<List<Article>> GetFrontPageArticlesAsync()
    {
        var articles = new List<Article>();
        
        // The cast to (ArticleType[]) is not necessary but it does make the code (marginally?) faster.
        // foreach (var type in (ArticleType[]) Enum.GetValues(typeof(ArticleType)))
        foreach (ArticleType type in Enum.GetValues(typeof(ArticleType)))
            articles.AddRange(await GetFrontPageArticlesAsync(type));

        return articles;
    }
    
    public async Task<List<Article>> GetFrontPageArticlesAsync(ArticleType articleType)
    {
        var response = await GetStringAsync();

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
                    // ToDo: replace all(?) HTML entities & normalise URIs(elsewhere) -- strip trailing ?...s
                    node.GetAttributeValue("title", "").Replace("&quot;", "\"").Replace("&amp;", "&"),
                    new Uri("https://www.dnevnik.bg" + node.GetAttributeValue("href", "")),
                    articleType
                );
                articles.Add(article);
            }
        }
        
        return articles;
    }

    private async Task<string> GetStringAsync(string? path = "/")
    {
        var response = "";

        try
        {
            response = await _client.GetStringAsync(path);
        }
        // System.Net.Http.HttpRequestException:
        //      Response status code does not indicate success:
        //      - 503 (Service Unavailable). -- usually Dnevnik
        //      - 403 (Forbidden). -- usually CloudFlare
        // ToDo: logging in 'Dramatist'?
        // ToDo: throw here? handle and log in the caller,
        // outside of 'Dramatist', not necessarily 'Taipi'
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
        }

        return response;
    }
}
