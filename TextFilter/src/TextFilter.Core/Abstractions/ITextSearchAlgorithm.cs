
namespace TextFilter.Core.Abstractions;

/// <summary>
/// Defines a method for determining whether a specified pattern exists within a given text using a particular string
/// comparison option.
/// </summary>
public interface ITextSearchAlgorithm
{
    bool Contains(string text, string pattern, StringComparison comparison = StringComparison.OrdinalIgnoreCase);
}
