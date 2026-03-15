using System;
using System.Collections.Generic;
using System.Text;

namespace TextFilter.Core.Abstractions;

/// <summary>
/// Defines a mechanism for determining whether a given word should be filtered according to custom criteria.
/// </summary>
public interface IWordFilter
{
    bool ShouldFilter(string word);
}

