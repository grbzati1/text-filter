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

        var tokenizer = new WordTokenizer();
        var sut = new TextFilterPipeline(filters);

        var words = tokenizer.Tokenize("This code gym sky atlas");
        var result = sut.Apply(words);

        Assert.Equal(new[] { "gym", "sky" }, result);
    }
}