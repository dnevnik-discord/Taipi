using Dramatist;
using Dramatist.Models;

namespace DramatistTest;

[TestClass]
public class AticleTest
{
    [TestMethod]
    public void TitleTestValid()
    {
        //-- Arrange
        Article article = new Article(
            "Горивата започнаха леко да поевтиняват, но дали това ще продължи",
            new Uri("https://www.dnevnik.bg/biznes/2022/08/20/4380620_gorivata_zapochnaha_leko_da_poevtiniavat_no_dali_tova/"),
            ArticleType.Lead
        );
        string expected = "Горивата започнаха леко да поевтиняват, но дали това ще продължи";

        //-- Act
        string actual = article.Title;

        //-- Assert
        Assert.AreEqual(expected, actual);
    }
}
