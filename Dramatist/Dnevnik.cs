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
    // ToDo: Why? How should be done?
    // public const Uri DnevnikBase = (Uri)"https://www.dnevnik.bg";
    public const string BaseAddress = "https://www.dnevnik.bg";
    public const string KnetworkBaseAddress = "https://knetwork.capital.bg";
    public const string IdCapitalBaseAddress = "https://id.capital.bg";

    public const string CapitalBaseAddress = "https://www.capital.bg";

    public static string XPathFromArticleType(ArticleType t)
    {
        switch (t)
        {
            case ArticleType.Lead:
                return "/html/body/div[3]/main/div/div[1]/div/div[1]/div/div[1]/article/h3/a";
            // ToDo: FIX ME!
            case ArticleType.SubLead:
                return "/html/body/div[3]/main/div/div[1]/div/div[1]/div/div[1]/div[2]/ul/li";
            case ArticleType.Important:
                return "/html/body/div[3]/main/div/div[1]/div/div[1]/div/div[1]/div[4]/div/div";
            case ArticleType.New:
                return "";
            case ArticleType.Analysis:
                return "";
            // ToDo: drop default?
            default:
                throw new Exception("Invalid article type!");
        }
    }
}
