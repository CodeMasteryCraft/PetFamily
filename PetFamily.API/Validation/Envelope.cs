using PetFamily.Domain.Common;

namespace PetFamily.API.Validation;

public class Envelope
{
    public object? Result { get; }
    public IReadOnlyCollection<Error>? Errors { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, IReadOnlyCollection<Error>? errors = null)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null)
    {
        return new(result);
    }

    public static Envelope Error(params Error?[] errors)
    {
        var filtered = errors.Where(item => item != null).Cast<Error>();
        return new(null, filtered.Any() ? filtered.ToArray() : null);
    }
}