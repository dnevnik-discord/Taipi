namespace Dramatist.Models;


public class Article
{
    // ToDo: Id and DnevnikId?
    private int _id;
    private Uri _shortUri;
    private Uri _fullUri;
    private string _title;

    public int Id { get => _id; }
    public Uri ShortUri { get => _shortUri; }
    public Uri FullUri { get => _fullUri; }
    public string Title { get => _title; }

    // ToDo: not really
    public Article()
    {
        throw new NotImplementedException();
    }

    public Article(int id)
    {
        _id = id;
        _shortUri = new Uri("https://www.dnevnik.bg/" + id);
        _fullUri = GetArticleFullUri();
        _title = GetArticleTitle();
    }

    private Uri GetArticleFullUri()
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
}
