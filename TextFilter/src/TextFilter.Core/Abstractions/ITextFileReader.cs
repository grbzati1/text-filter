namespace TextFilter.Core.Abstractions;

/// <summary>
/// Defines a method for asynchronously reading the entire contents of a text file.
/// </summary>
public interface ITextFileReader
{
    Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);
}
