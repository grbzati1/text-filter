namespace TextFilter.Core.Common;

/// <summary>
/// Provides guard clause methods for validating method arguments.
/// </summary>
internal static class Guard
{
    public static void AgainstNullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", paramName);
        }
    }
}

