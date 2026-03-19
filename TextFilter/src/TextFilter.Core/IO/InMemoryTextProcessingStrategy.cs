using System;
using System.Collections.Generic;
using System.Text;
using TextFilter.Core.Abstractions;
using TextFilter.Core.Processing;

namespace TextFilter.Core.IO;

public sealed class InMemoryTextProcessingStrategy : ITextProcessingStrategy
{
    private readonly ITextFileReader _reader;
    private readonly IWordTokenizer _tokenizer;
    private readonly TextFilterPipeline _pipeline;

    public InMemoryTextProcessingStrategy(
        ITextFileReader reader,
        IWordTokenizer tokenizer,
        TextFilterPipeline pipeline)
    {
        _reader = reader;
        _tokenizer = tokenizer;
        _pipeline = pipeline;
    }

    public async IAsyncEnumerable<string> ProcessTextAsync(
        string path,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var text = await _reader.ReadAllTextAsync(path, cancellationToken);
        var words = _tokenizer.Tokenize(text);
        var filtered = _pipeline.Apply(words);

        foreach (var word in filtered)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return word;
        }
    }
}