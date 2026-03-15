using TextFilter.Core.Filters;
using Xunit;

namespace TextFilter.Tests.Filters;

/// <summary>
/// Contains unit tests for the MiddleVowelFilter class, verifying its behavior when filtering words based on the
/// presence of a vowel in the middle position.
/// </summary>
public sealed class MiddleVowelFilterTests
{
    [Fact]
    public void ShouldFilter_ReturnsTrue_ForOddLengthWordWithMiddleVowel()
    {
        var sut = new MiddleVowelFilter();

        Assert.True(sut.ShouldFilter("cat"));
    }

    [Fact]
    public void ShouldFilter_ReturnsTrue_ForEvenLengthWordWithMiddleVowel()
    {
        var sut = new MiddleVowelFilter();

        Assert.True(sut.ShouldFilter("steam"));
    }

    [Fact]
    public void ShouldFilter_ReturnsFalse_WhenMiddleDoesNotContainVowel()
    {
        var sut = new MiddleVowelFilter();

        Assert.False(sut.ShouldFilter("rhythm"));
    }
}
