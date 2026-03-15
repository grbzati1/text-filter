using TextFilter.Core.Abstractions;
using TextFilter.Core.Configuration;

namespace TextFilter.Core.Search;

/// <summary>
/// Provides a factory for creating text search algorithm instances based on the specified algorithm type and pattern.
/// </summary>
public sealed class TextSearchAlgorithmFactory : ITextSearchAlgorithmFactory
{
    private readonly NaiveTextSearchAlgorithm _naive;
    private readonly SingleCharacterSearchAlgorithm _singleCharacter;

    public TextSearchAlgorithmFactory(
        NaiveTextSearchAlgorithm naive,
        SingleCharacterSearchAlgorithm singleCharacter)
    {
        _naive = naive ?? throw new ArgumentNullException(nameof(naive));
        _singleCharacter = singleCharacter ?? throw new ArgumentNullException(nameof(singleCharacter));
    }

    public ITextSearchAlgorithm Create(TextSearchAlgorithmType algorithmType, string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        return algorithmType switch
        {
            TextSearchAlgorithmType.Naive => _naive,
            TextSearchAlgorithmType.SingleCharacter => _singleCharacter,
            TextSearchAlgorithmType.Auto => SelectAutomatically(pattern),
            _ => throw new ArgumentOutOfRangeException(
                nameof(algorithmType),
                algorithmType,
                "Unknown search algorithm type.")
        };
    }

    private ITextSearchAlgorithm SelectAutomatically(string pattern)
    {
        return pattern.Length == 1 ? _singleCharacter : _naive;
    }
}