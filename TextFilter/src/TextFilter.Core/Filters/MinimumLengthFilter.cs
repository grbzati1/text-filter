using TextFilter.Core.Abstractions;
using TextFilter.Core.Common;

namespace TextFilter.Core.Filters;

/// <summary>
/// Represents a word filter that excludes words shorter than a specified minimum length.
/// </summary>
public sealed class MinimumLengthFilter : IWordFilter
{
    private readonly int _minimumLength;

    public MinimumLengthFilter(int minimumLength)
    {
        if (minimumLength < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumLength));
        }

        _minimumLength = minimumLength;
    }

    public bool ShouldFilter(string word)
    {
        Guard.AgainstNullOrWhiteSpace(word, nameof(word));
        return word.Length < _minimumLength;
    }
}
