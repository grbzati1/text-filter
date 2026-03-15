using System.Text.RegularExpressions;
using TextFilter.Core.Abstractions;

namespace TextFilter.Core.Processing;

/// <summary>
/// Provides functionality to tokenize a text string into individual words using regular expression-based parsing.
/// </summary>
public sealed class WordTokenizer : IWordTokenizer
{
    private static readonly Regex WordRegex = new("[A-Za-z]+", RegexOptions.Compiled);

    public IReadOnlyList<string> Tokenize(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var list = WordRegex  
            .Matches(text)
            .Cast<Match>()
            .Select(match => match.Value)
            .ToArray();

        return list;
    }
}