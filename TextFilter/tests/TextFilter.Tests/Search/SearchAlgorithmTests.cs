using TextFilter.Core.Abstractions;
using TextFilter.Core.Configuration;
using TextFilter.Core.Search;
using Xunit;

namespace TextFilter.Tests.Search;

/// <summary>
/// Contains unit tests for verifying the behavior of text search algorithm implementations and their factory selection
/// logic.
/// </summary>
public sealed class SearchAlgorithmTests
{
    [Theory]
    [InlineData("Alphabet", "pha")]
    [InlineData("Alphabet", "bet")]
    public void Naive_FindsPattern_WhenPresent(string text, string pattern)
    {
        var sut = new NaiveTextSearchAlgorithm();

        Assert.True(sut.Contains(text, pattern));
    }

    [Theory]
    [InlineData("Alphabet", "t")]
    [InlineData("Alphabet", "A")]
    public void SingleCharacter_FindsPattern_WhenPresent(string text, string pattern)
    {
        var sut = new SingleCharacterSearchAlgorithm();

        Assert.True(sut.Contains(text, pattern));
    }

    [Theory]
    [InlineData("Alphabet", "pha")]
    [InlineData("Alphabet", "bet")]
    public void SingleCharacter_Throws_WhenPatternLengthIsGreaterThanOne(string text, string pattern)
    {
        var sut = new SingleCharacterSearchAlgorithm();

        Assert.Throws<ArgumentException>(() => sut.Contains(text, pattern));
    }

    [Fact]
    public void Factory_Auto_UsesSingleCharacter_ForSingleCharacterPatterns()
    {
        var sut = new TextSearchAlgorithmFactory(
            new NaiveTextSearchAlgorithm(),
            new SingleCharacterSearchAlgorithm());

        var algorithm = sut.Create(TextSearchAlgorithmType.Auto, "t");

        Assert.IsType<SingleCharacterSearchAlgorithm>(algorithm);
    }

    [Fact]
    public void Factory_Auto_UsesNaive_ForLongerPatterns()
    {
        var sut = new TextSearchAlgorithmFactory(
            new NaiveTextSearchAlgorithm(),
            new SingleCharacterSearchAlgorithm());

        var algorithm = sut.Create(TextSearchAlgorithmType.Auto, "term");

        Assert.IsType<NaiveTextSearchAlgorithm>(algorithm);
    }
}
