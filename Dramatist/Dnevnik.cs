namespace Dramatist;


public enum ArticleType
{
    Lead,
    SubLead,
    Important,
    New,
    Analysis
}


public enum AvatarType
{
    Big,
    Medium,
    Small,
    Tiny
}


public static class Dnevnik
{
    public const string BaseAddress = "https://www.dnevnik.bg";

    public const string KnetworkBaseAddress = "https://knetwork.capital.bg";
    
    public const string IdBaseAddress = "https://id.capital.bg";

    public const string CapitalBaseAddress = "https://www.capital.bg";

    public static string XPathFromArticleType(ArticleType t)
    {
        switch (t)
        {
            // ToDo: FIX ME!
            case ArticleType.Lead:
                return "//article[@class='primary-article-v1']/h3/a";
            case ArticleType.SubLead:
                return "//div/div[1]/div/div[1]/div[2]/ul/li/article/h3/a";
            case ArticleType.Important:
                return "//div/div[1]/div/div[1]/div[position()=3 or position()=4]/div/div[position()=1 or position()=2]/article/h3/a";
            case ArticleType.New:
                return "//div[@class='list-of-latest-stories']/article[@class='secondary-article']/h3/a";
            case ArticleType.Analysis:
                return "//div/div[2]/div/div[1]/div/div[2]/h3/a";
            // ToDo: drop default?
            default:
                throw new Exception("Invalid article type!");
        }
    }
}
