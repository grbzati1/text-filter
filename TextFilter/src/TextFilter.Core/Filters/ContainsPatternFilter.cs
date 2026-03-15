using TextFilter.Core.Abstractions;
using TextFilter.Core.Common;

namespace TextFilter.Core.Filters;

/// <summary>
/// Represents a word filter that determines whether a word should be filtered based on whether it contains a specified
/// pattern using a configurable text search algorithm.
/// </summary>
public sealed class ContainsPatternFilter : IWordFilter
{
    private readonly string _pattern;
    private readonly ITextSearchAlgorithm _searchAlgorithm;
    private readonly StringComparison _comparison;

    public ContainsPatternFilter(
        string pattern,
        ITextSearchAlgorithm searchAlgorithm,
        StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        Guard.AgainstNullOrWhiteSpace(pattern, nameof(pattern));
        ArgumentNullException.ThrowIfNull(searchAlgorithm);

        _pattern = pattern;
        _searchAlgorithm = searchAlgorithm;
        _comparison = comparison;
    }

    public bool ShouldFilter(string word)
    {
        Guard.AgainstNullOrWhiteSpace(word, nameof(word));
        return _searchAlgorithm.Contains(word, _pattern, _comparison);
    }
}
