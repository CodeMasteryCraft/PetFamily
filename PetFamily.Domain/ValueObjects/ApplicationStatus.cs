using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using ValueObject = PetFamily.Domain.Common.ValueObject;

namespace PetFamily.Domain.ValueObjects;

public class ApplicationStatus : ValueObject
{
    public static readonly ApplicationStatus Denied = new(nameof(Denied).ToUpper());
    public static readonly ApplicationStatus Consideration = new(nameof(Consideration).ToUpper());
    public static readonly ApplicationStatus Approved = new(nameof(Approved).ToUpper());

    private static readonly ApplicationStatus[] _all = [Denied, Consideration, Approved];

    public string Status { get; }

    private ApplicationStatus(string status)
    {
        Status = status;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Status;
    }
}