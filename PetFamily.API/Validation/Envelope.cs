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
    

    private Envelope(object? result, List<Error>? error)
    {
        Result = result;

        if (error is not null)
        {
            foreach (var er in error) 
            { 
                _errorCode.Add(er.Code);
                _errorMessage.Add(er.Message);
                _errors.Add(er);
            }
        }

        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null)
    {
        return new(result, null);
    }

    public static Envelope Error(List<Error> error)
    {
        return new(null, error);
    }
}