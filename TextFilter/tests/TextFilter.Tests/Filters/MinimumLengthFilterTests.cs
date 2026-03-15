using TextFilter.Core.Filters;
using Xunit;

namespace TextFilter.Tests.Filters;

/// <summary>
/// Contains unit tests for the MinimumLengthFilter class to verify its filtering behavior based on word length.
/// </summary>
public sealed class MinimumLengthFilterTests
{
    [Fact]
    public void ShouldFilter_ReturnsTrue_WhenWordIsShorterThanMinimum()
    {
        var sut = new MinimumLengthFilter(4);

        Assert.True(sut.ShouldFilter("cat"));
    }

    [Fact]
    public void ShouldFilter_ReturnsFalse_WhenWordMeetsMinimumLength()
    {
        var sut = new MinimumLengthFilter(4);

        Assert.False(sut.ShouldFilter("code"));
    }
}
