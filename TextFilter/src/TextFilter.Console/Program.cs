namespace TextFilter.Console;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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

        var reader = host.Services.GetRequiredService<ITextFileReader>();
        var pipeline = host.Services.GetRequiredService<TextFilterPipeline>();

        var inputText = await reader.ReadAllTextAsync(inputPath).ConfigureAwait(false);
        var outputText = pipeline.Apply(inputText);

        System.Console.WriteLine("Filtered text:");
        System.Console.WriteLine(outputText);

        return 0;
    }
}
