namespace TextFilter.Core.Configuration;

/// <summary>
/// Represents the options used to configure a text search operation.
/// </summary>
public sealed class SearchOptions
{
    public string Pattern { get; set; } = "t";
    public TextSearchAlgorithmType Algorithm { get; set; } = TextSearchAlgorithmType.Auto;
}
