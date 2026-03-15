using TextFilter.Core.Abstractions;
using TextFilter.Core.Filters;
using TextFilter.Core.Processing;
using TextFilter.Core.Search;
using Xunit;

namespace TextFilter.Tests.Processing;

/// <summary>
/// Contains unit tests for the TextFilterPipeline class to verify its filtering behavior.
/// </summary>
public sealed class TextFilterPipelineTests
{
    [Fact]
    public void Apply_RemovesWordsMatchingConfiguredFilters()
    {
        IWordFilter[] filters =
        {
            new MinimumLengthFilter(3),
            new MiddleVowelFilter(),
            new ContainsPatternFilter("t", new NaiveTextSearchAlgorithm())
        };

        var sut = new TextFilterPipeline(filters, new WordTokenizer());

        var result = sut.Apply("This code gym sky atlas");

        Assert.Equal("gym sky", result);
    }
}