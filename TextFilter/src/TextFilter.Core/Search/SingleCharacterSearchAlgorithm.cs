using TextFilter.Core.Abstractions;

namespace TextFilter.Core.Search;

/// <summary>
/// Provides a text search algorithm optimized for detecting the presence of a single character pattern within a string.
/// </summary>
public sealed class SingleCharacterSearchAlgorithm : ITextSearchAlgorithm
{
    public bool Contains(string text, string pattern, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(pattern);

        if (pattern.Length == 0)
            return true;

        if (pattern.Length != 1)
            throw new ArgumentException(
                "SingleCharacterSearchAlgorithm only supports patterns of length 1.",
                nameof(pattern));

        return text.Contains(pattern, comparison);
    }

}