using System;
using System.Collections.Generic;
using System.Text;
using TextFilter.Core.Abstractions;
using TextFilter.Core.Processing;

namespace TextFilter.Core.IO;

public sealed class StreamingTextProcessingStrategy : ITextProcessingStrategy
{
    private readonly IWordTokenizer _tokenizer;
    private readonly TextFilterPipeline _pipeline;

    public StreamingTextProcessingStrategy(
        IWordTokenizer tokenizer,
        TextFilterPipeline pipeline)
    {
        _tokenizer = tokenizer;
        _pipeline = pipeline;
    }

    public async IAsyncEnumerable<string> ProcessTextAsync(
        string path,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(path);

        using var reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var words = _tokenizer.Tokenize(line);
            var filtered = _pipeline.Apply(words);

            foreach (var word in filtered)
            {
                yield return word;
            }
        }
    }

  
}