using System.Data;

namespace PetFamily.Domain.Common;

public class Error
{
    private const string Separator = "||";

    public string Code { get; }
    public string? Message { get; }

    public Error(string code, string? message = null)
    {
        Code = code;
        Message = message;
    }

    public string Serialize()
    {
        return $"{Code}{Separator}{Message}";
    }

    public static Error Deserialize(string serialized)
    {
        var data = serialized.Split([Separator], StringSplitOptions.RemoveEmptyEntries);

        if (data.Length < 2)
            throw new($"Invalid error serialization: '{serialized}'");

        return new(data[0], data[1]);
    }
}

public static class Seccess
{
    public static Error Ok() =>
        new ("Seccess");
}

public static class Errors
{
    public static class General
    {
        public static Error Iternal(string message)
            => new("iternal", message);
        
        public static Error Unexpected()
            => new("unexpecret", "unexpecret");

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return new("record.not.found", $"record not found{forId}");
        }

        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "Value";
            return new("value.is.invalid", $"{label} is invalid");
        }

        public static Error ValueIsRequried(string? name = null)
        {
            var label = name ?? "Value";
            return new("value.is.required", $"{label} is required");
        }

        public static Error InvalidLength(string? name = null)
        {
            var label = name == null ? " " : " " + name + " ";
            return new("length.is.invalid", $"invalid{label}length");
        }

        public static Error SaveFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.save.failure", $"{label} failed to save");
        }

        public static Error DeleteFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.delete.failure", $"{label} failed to delete");
        }

        public static Error GetFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.get.failure",$"{label} failed to get");
        }
    }

    public static class Volunteers
    {
        public static Error PhotoCountLimit()
        {
            return new("volunteers.photo.limit", "Max photo count limit is 5");
        }

    }
}