using TextFilter.Core.Configuration;

namespace TextFilter.Core.Abstractions;

public interface ITextSearchAlgorithmFactory
{
    ITextSearchAlgorithm Create(TextSearchAlgorithmType algorithmType, string pattern);
}
