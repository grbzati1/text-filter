using TextFilter.Core.Abstractions;
using TextFilter.Core.Common;

namespace TextFilter.Core.IO;

/// <summary>
/// Provides functionality for asynchronously reading the contents of a text file.
/// </summary>
public sealed class TextFileReader : ITextFileReader
{
    public async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrWhiteSpace(path, nameof(path));
        return await File.ReadAllTextAsync(path, cancellationToken).ConfigureAwait(false);
    }
}
