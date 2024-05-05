using System.Data;

namespace PetFamily.Domain.Common;

public class ResultEvent
{
    private const string Separator = "||";

    public string Code { get; }
    public string? Message { get; }

    public ResultEvent(string code, string? message = null)
    {
        Code = code;
        Message = message;
    }

    public string Serialize()
    {
        return $"{Code}{Separator}{Message}";
    }

    public static ResultEvent Deserialize(string serialized)
    {
        var data = serialized.Split([Separator], StringSplitOptions.RemoveEmptyEntries);

        if (data.Length < 2)
            throw new($"Invalid error serialization: '{serialized}'");

        return new(data[0], data[1]);
    }
}

public static class Seccess
{
    public static ResultEvent Ok() =>
        new ("Seccess");
}

public static class Errors
{
    public static class General
    {
        public static ResultEvent Iternal(string message)
            => new("iternal", message);
        
        public static ResultEvent Unexpected()
            => new("unexpecret", "unexpecret");

        public static ResultEvent NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return new("record.not.found", $"record not found{forId}");
        }

        public static ResultEvent ValueIsInvalid(string? name = null)
        {
            var label = name ?? "Value";
            return new("value.is.invalid", $"{label} is invalid");
        }

        public static ResultEvent ValueIsRequried(string? name = null)
        {
            var label = name ?? "Value";
            return new("value.is.required", $"{label} is required");
        }

        public static ResultEvent InvalidLength(string? name = null)
        {
            var label = name == null ? " " : " " + name + " ";
            return new("length.is.invalid", $"invalid{label}length");
        }

        public static ResultEvent SaveFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.save.failure", $"{label} failed to save");
        }

        public static ResultEvent DeleteFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.delete.failure", $"{label} failed to delete");
        }

        public static ResultEvent GetFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.get.failure",$"{label} failed to get");
        }
    }

    public static class Volunteers
    {
        public static ResultEvent PhotoCountLimit()
        {
            return new("volunteers.photo.limit", "Max photo count limit is 5");
        }

    }
}