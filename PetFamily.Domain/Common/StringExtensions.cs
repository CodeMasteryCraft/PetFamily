namespace PetFamily.Domain.Common;

public static class StringExtensions
{
    public static bool IsEmpty(this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
}