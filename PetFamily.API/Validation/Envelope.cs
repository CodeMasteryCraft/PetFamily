using PetFamily.Domain.Common;

namespace PetFamily.API.Validation;

public class Envelope
{
    public object? Result { get; }

    public IReadOnlyList<string> ErrorCode => _errorCode;
    private readonly List<string> _errorCode = [];

    public IReadOnlyList<string> ErrorMessage => _errorMessage;
    private readonly List<string> _errorMessage = [];

    public DateTime TimeGenerated { get; }

    public IReadOnlyList<Error> Errors => _errors;
    private readonly List<Error> _errors = [];


    private Envelope(object? result, List<Error>? errors)
    {
        Result = result;

        if (errors is not null)
        {
            _errorCode.AddRange(errors.Select(er => er.Code));
            _errorMessage.AddRange(errors.Select(er => er.Message));
            _errors.AddRange(errors);
        }

        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null)
    {
        return new(result, null);
    }

    public static Envelope Error(List<Error>? error)
    {
        return new(null, error);
    }
}