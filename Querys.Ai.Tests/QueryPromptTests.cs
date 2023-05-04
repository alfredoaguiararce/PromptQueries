[TestFixture]
public class QueryPromptTests
{
    private QueryPrompt queryPrompt;

    [SetUp]
    public void Setup()
    {
        queryPrompt = new QueryPrompt("");
    }

    [Test]
    public async Task TestQueryPromptReturnsValidResult()
    {
        // Arrange
        var data = new List<int> { 1, 2, 3, 4, 5 }.AsQueryable();
        string prompt = "Filter out items that are greater than 3.";

        // Act
        var result = await queryPrompt.RunQuery(data, prompt);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Contains(4));
        Assert.IsTrue(result.Contains(5));
    }

    [Test]
    public void TestQueryPromptThrowsExceptionForInvalidQuery()
    {
        // Arrange
        var data = new List<int> { 1, 2, 3, 4, 5 }.AsQueryable();
        string prompt = "Filter the items where each item is increased by 1";

        // Act and Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => queryPrompt.RunQuery(data, prompt));
    }
}
