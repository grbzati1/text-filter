using System;
using System.Collections.Generic;
using System.Text;

namespace TextFilter.Core.Configuration;

public sealed class ProcessingOptions
{
    public string Mode { get; set; } = "Auto";
    public long MaxInMemoryFileSizeBytes { get; set; } = 1_048_576;
}