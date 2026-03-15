using TextFilter.Core.Filters;
using TextFilter.Core.Search;
using Xunit;

namespace TextFilter.Tests.Filters;

/// <summary>
/// Contains unit tests for the ContainsPatternFilter class to verify its filtering behavior based on pattern existence
/// in input text.
/// </summary>
public sealed class ContainsPatternFilterTests
{
    [Fact]
    public void ShouldFilter_ReturnsTrue_WhenPatternExists()
    {
        var sut = new ContainsPatternFilter("t", new NaiveTextSearchAlgorithm());

        Assert.True(sut.ShouldFilter("text"));
    }

    [Fact]
    public void ShouldFilter_ReturnsFalse_WhenPatternDoesNotExist()
    {
        var sut = new ContainsPatternFilter("z", new NaiveTextSearchAlgorithm());

        Assert.False(sut.ShouldFilter("text"));
    }
}
