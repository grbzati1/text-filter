using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;
using TextFilter.Core.Abstractions;
using TextFilter.Core.Search;

BenchmarkRunner.Run<SearchAlgorithmBenchmarks>();
/// <summary>
/// Provides benchmark tests for comparing the performance of different text search algorithm implementations.
/// </summary>
/// <remarks>This class is intended for use with BenchmarkDotNet to measure and compare the efficiency of various
/// ITextSearchAlgorithm strategies when searching for single and multiple character patterns within a large text. The
/// benchmarks can help identify the most suitable algorithm for specific search scenarios.</remarks>
[MemoryDiagnoser]
public class SearchAlgorithmBenchmarks
{
    private readonly string _text = string.Concat(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 200));

    private readonly ITextSearchAlgorithm _naive = new NaiveTextSearchAlgorithm();
    private readonly ITextSearchAlgorithm _singleChar = new SingleCharacterSearchAlgorithm();

    private readonly string _singlePattern = "t";
    private readonly string _multiPattern = "mnopq";

    [Benchmark]
    public bool SingleCharacter()
    {
        return _singleChar.Contains(_text, _singlePattern);
    }

    [Benchmark]
    public bool Naive_MultiCharacter()
    {
        return _naive.Contains(_text, _multiPattern);
    }
}
