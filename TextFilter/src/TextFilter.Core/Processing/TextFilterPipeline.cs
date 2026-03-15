using TextFilter.Core.Abstractions;

namespace TextFilter.Core.Processing;

/// <summary>
/// Represents a pipeline that applies one or more word filters to input text using a specified tokenizer.
/// </summary>
public sealed class TextFilterPipeline
{
    private readonly IReadOnlyCollection<IWordFilter> _filters;
    private readonly IWordTokenizer _tokenizer;

    public TextFilterPipeline(IEnumerable<IWordFilter> filters, IWordTokenizer tokenizer)
    {
        ArgumentNullException.ThrowIfNull(filters);
        ArgumentNullException.ThrowIfNull(tokenizer);

        _filters = filters.ToArray();
        _tokenizer = tokenizer;

        if (_filters.Count == 0)
        {
            throw new ArgumentException("At least one filter must be provided.", nameof(filters));
        }
    }

    public string Apply(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var tokenizedWords = _tokenizer.Tokenize(text);
        var remainingWords = new List<string>();

        foreach (var word in tokenizedWords)
        {
            var shouldFilter = false;

            foreach (var filter in _filters)
            {
                if (filter.ShouldFilter(word))
                {
                    shouldFilter = true;
                    break;
                }
            }

            if (!shouldFilter)
            {
                remainingWords.Add(word);
            }
        }

        return string.Join(' ', remainingWords);
    }
}
