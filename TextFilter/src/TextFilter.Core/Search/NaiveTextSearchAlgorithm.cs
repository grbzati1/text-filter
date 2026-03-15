using TextFilter.Core.Abstractions;

namespace TextFilter.Core.Search;


/// <summary>
/// Provides a simple, naive implementation of the text search algorithm that checks for the presence of a pattern
/// within a given text using direct character comparison.
/// </summary>
public sealed class NaiveTextSearchAlgorithm : ITextSearchAlgorithm
{
    public bool Contains(string text, string pattern, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(pattern);

        var textLength = text.Length;
        var patternLength = pattern.Length;

        if (patternLength == 0)
            return true;

        if (patternLength > textLength)
            return false;

        for (var start = 0; start <= textLength - patternLength; start++)
        {
            var matched = true;

            for (var offset = 0; offset < patternLength; offset++)
            {
                if (!CharsEqual(text[start + offset], pattern[offset], comparison))
                {
                    matched = false;
                    break;
                }
            }

            if (matched)
                return true;
        }

        return false;
    }
   
    private static bool CharsEqual(char left, char right, StringComparison comparison)
    {
        return comparison switch
        {
            StringComparison.Ordinal => left == right,
            StringComparison.OrdinalIgnoreCase =>
                char.ToUpperInvariant(left) == char.ToUpperInvariant(right),
            _ => throw new NotSupportedException(
                $"StringComparison '{comparison}' is not supported by this search implementation.")
        };
    }
}
