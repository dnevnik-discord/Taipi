namespace Dramatist.Models;


public class Comment
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int Position { get; set; }
    public int UserId { get; set; }
    public string? Contents { get; set; }

    public Comment()
    {
        throw new NotImplementedException();
    }

    Comment(int id, int articleId, int position, int userId, string? contents = null)
    {
        Id = id;
        ArticleId = articleId;
        Position = position;
        UserId = userId;
        Contents = contents;
    }
}
