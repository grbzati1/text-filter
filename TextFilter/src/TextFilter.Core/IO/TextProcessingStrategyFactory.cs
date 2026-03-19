using TextFilter.Core.Abstractions;
using TextFilter.Core.Configuration;
using Microsoft.Extensions.Options;

namespace TextFilter.Core.IO;

public sealed class TextProcessingStrategyFactory
{
    private readonly InMemoryTextProcessingStrategy _inMemory;
    private readonly StreamingTextProcessingStrategy _streaming;
    private readonly ProcessingOptions _options;

    public TextProcessingStrategyFactory(
        InMemoryTextProcessingStrategy inMemory,
        StreamingTextProcessingStrategy streaming,
        IOptions<ProcessingOptions> options)
    {
        _inMemory = inMemory;
        _streaming = streaming;
        _options = options.Value;
    }

    public ITextProcessingStrategy Create(string path)
    {
        var fileInfo = new FileInfo(path);

        return _options.Mode switch
        {
            "InMemory" => _inMemory,
            "Streaming" => _streaming,
            "Auto" => fileInfo.Length <= _options.MaxInMemoryFileSizeBytes
                ? _inMemory
                : _streaming,
            _ => throw new InvalidOperationException(
                $"Unsupported processing mode '{_options.Mode}'.")
        };
    }
}