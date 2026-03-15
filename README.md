# TextFilter

A .NET 10 solution for filtering words from a text file based on three rules:

1. remove words shorter than a minimum length
2. remove words whose middle character(s) contain a vowel
3. remove words containing a configured pattern

The solution keeps the core logic in a reusable library, wires dependencies in the console app using DI, includes xUnit tests, and contains BenchmarkDotNet benchmarks for the search algorithms.

## Projects

- `TextFilter.Core` - core abstractions, filters, tokenization, pipeline, search algorithms
- `TextFilter.Console` - CLI entry point and dependency injection composition root
- `TextFilter.Tests` - unit tests
- `TextFilter.Benchmarks` - performance comparison for search strategies

## Run

```bash
 dotnet run --project src/TextFilter.Console -- input/sample.txt
```

## Test

```bash
 dotnet test --collect:"XPlat Code Coverage"
```

## Benchmark

```bash
 dotnet run -c Release --project benchmarks/TextFilter.Benchmarks/TextFilter.Benchmarks.csproj
```

### Benchmark results

Benchmarking showed that the dedicated single-character search performed faster than the general naive multi-character search for the tested input, while neither implementation allocated managed memory. This supports using a specialised single-character path for the current requirement, where the searched pattern is a single character.

## Future improvements

- Add a streaming mode for large files to reduce memory usage.
- Support InMemory, Streaming, and Auto processing modes based on file size.
- Expand tokenization rules and document edge cases more clearly.
- Make filter settings fully configuration-driven.
- Add lightweight logging and better diagnostics.
- Extend benchmarks to cover larger files and memory allocations.
- Add integration tests for console execution and file handling.
- Add algorithms such as Boyer–Moore–Horspool for longer patterns if search requirements become more complex.
- Support optional structured output such as JSON or file output.
- Improve validation and error reporting.
