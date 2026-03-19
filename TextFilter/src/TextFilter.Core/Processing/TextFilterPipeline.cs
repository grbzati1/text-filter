using TextFilter.Core.Abstractions;

namespace TextFilter.Core.Processing;

/// <summary>
/// Represents a pipeline that applies one or more word filters to input text using a specified tokenizer.
/// </summary>
public sealed class TextFilterPipeline
{
    private readonly IEnumerable<IWordFilter> _filters;

    public TextFilterPipeline(IEnumerable<IWordFilter> filters)
    {
        _filters = filters;
    }

    public IReadOnlyList<string> Apply(IEnumerable<string> words)
    {
        var result = new List<string>();

        foreach (var word in words)
        {
            var shouldKeep = true;

            foreach (var filter in _filters)
            {
                if (filter.ShouldFilter(word))
                {
                    shouldKeep = false;
                    break;
                }
            }

            if (shouldKeep)
            {
                result.Add(word);
            }
        }

        return result;
    }
}