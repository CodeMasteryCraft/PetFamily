using PetFamily.Domain.Common;

namespace PetFamily.API.Contracts;

public class Envelope
{
    public object? Result { get; }
    public List<ErrorInfo>? ErrorInfo { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, IEnumerable<ErrorInfo>? errors)
    {
        Result = result;
        ErrorInfo = errors?.ToList();
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null)
    {
        return new(result, null);
    }
    
    public static Envelope Error(List<ResultEvent>? errors = null)
    {
        var errorInfos = errors?.Select(e => new ErrorInfo(e));
        return new(null, errorInfos);
    }

    public static Envelope Error(List<ErrorInfo>? errors)
    {
        return new(null, errors);
    }
}

public class ErrorInfo
{
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }
    public string? InvalidField { get; }

    public ErrorInfo(ResultEvent? error, string? invalidField = null)
    {
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        InvalidField = invalidField;
    }
}