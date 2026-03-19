using System;
using System.Collections.Generic;
using System.Text;

namespace TextFilter.Core.Abstractions;

public interface  ITextProcessingStrategy
{

    IAsyncEnumerable<string> ProcessTextAsync(string path, CancellationToken cancellationToken = default);

}
