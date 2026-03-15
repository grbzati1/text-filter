using TextFilter.Core.Abstractions;
using TextFilter.Core.Common;

namespace TextFilter.Core.Filters;

/// <summary>
/// Provides a word filter that identifies words containing a vowel in the middle position.
/// </summary>
public sealed class MiddleVowelFilter : IWordFilter
{
    private static readonly HashSet<char> Vowels = new("aeiouAEIOU");

    public bool ShouldFilter(string word)
    {
        Guard.AgainstNullOrWhiteSpace(word, nameof(word));

        var span = word.AsSpan();
        var middleLength = span.Length % 2 == 0 ? 2 : 1;
        var start = (span.Length - middleLength) / 2;

        for (var index = start; index < start + middleLength; index++)
        {
            if (Vowels.Contains(span[index]))
            {
                return true;
            }
        }

        return false;
    }
}