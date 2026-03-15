namespace TextFilter.Core.Abstractions;

/// <summary>
/// Defines a contract for splitting text into individual word tokens.
/// </summary>
public interface IWordTokenizer
{
    IReadOnlyList<string> Tokenize(string text);
}
