namespace TextFilter.Console;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TextFilter.Core.Abstractions;
using TextFilter.Core.Configuration;
using TextFilter.Core.Filters;
using TextFilter.Core.IO;
using TextFilter.Core.Processing;
using TextFilter.Core.Search;


/// <summary>
/// Provides the entry point for the text filtering.
/// </summary>
internal class Program
{
    static async Task<int> Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.Configure<TextFilterOptions>(builder.Configuration.GetSection("TextFilter"));
        builder.Services.Configure<SearchOptions>(builder.Configuration.GetSection("Search"));
        builder.Services.Configure<ProcessingOptions>(builder.Configuration.GetSection("Processing"));

        builder.Services.AddSingleton<InMemoryTextProcessingStrategy>();
        builder.Services.AddSingleton<StreamingTextProcessingStrategy>();
        builder.Services.AddSingleton<TextProcessingStrategyFactory>();

        builder.Services.AddSingleton<ITextFileReader, TextFileReader>();
        builder.Services.AddSingleton<IWordTokenizer, WordTokenizer>();

        builder.Services.AddSingleton<NaiveTextSearchAlgorithm>();
        builder.Services.AddSingleton<SingleCharacterSearchAlgorithm>();
        builder.Services.AddSingleton<ITextSearchAlgorithmFactory, TextSearchAlgorithmFactory>();

        builder.Services.AddSingleton<IWordFilter>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<TextFilterOptions>>().Value;
            return new MinimumLengthFilter(options.MinimumLength);
        });

        builder.Services.AddSingleton<IWordFilter, MiddleVowelFilter>();

        builder.Services.AddSingleton<IWordFilter>(sp =>
        {
            var searchOptions = sp.GetRequiredService<IOptions<SearchOptions>>().Value;
            var factory = sp.GetRequiredService<ITextSearchAlgorithmFactory>();
            var algorithm = factory.Create(searchOptions.Algorithm, searchOptions.Pattern);

            return new ContainsPatternFilter(searchOptions.Pattern, algorithm);
        });

        builder.Services.AddSingleton<TextFilterPipeline>();

        using var host = builder.Build();

        var inputPath = args.Length > 0
            ? args[0]
            : Path.Combine(AppContext.BaseDirectory, "input", "sample.txt");

        if (!File.Exists(inputPath))
        {
            System.Console.Error.WriteLine($"Input file not found: {inputPath}");
            return 1;
        }

       
        var strategyFactory = host.Services.GetRequiredService<TextProcessingStrategyFactory>();
        var strategy = strategyFactory.Create(inputPath);


        var results = new List<string>();
        CancellationToken cancellationToken = default;

        await foreach (var word in strategy.ProcessTextAsync(inputPath, cancellationToken))
        {
            results.Add(word);
        }

        System.Console.WriteLine($"Filtered text: " + string.Join(' ', results));


        return 0;
    }
}
