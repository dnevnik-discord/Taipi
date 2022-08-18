namespace Dramatist.Models;


public class Article
{
    // ToDo: Id and DnevnikId?
    public int Id { get; set; }
    public string Title { get; set; }
    public Uri Uri { get; set; }
    public Uri ?ShortUri { get; set; } = null;
    public ArticleType ?ArticleType { get; set; }

    // ToDo: not really
    public Article(Uri uri)
    {
        throw new NotImplementedException();
    }

    public Article(string title, Uri uri, ArticleType? articleType = null)
    {
        Title = title;
        Uri = uri;
        ArticleType = articleType;
    }

    public Article(int id)
    {
        Id = id;
        Title = GetArticleTitle();
        Uri = GetArticleUri(id);
        ShortUri = new Uri("https://www.dnevnik.bg/" + id);
    }

    private Uri GetArticleUri(int id)
    {
        // from _shortUri
        throw new NotImplementedException();
    }

    // ToDo: by FullUri only?
    private string GetArticleTitle()
    {
        throw new NotImplementedException();
    }

    public List<Comment> GetComments()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"Title: {Title} | Uri: {Uri}";
    }

    // from Article.Uri
    private string GetId()
    {
        throw new NotImplementedException();
    }
}
