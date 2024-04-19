namespace PetFamily.Domain.Common;

public class Error
{
    public string Code { get; set; }

    public string Message { get; set; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}

public static class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return new("record.not.found", $"record not found{forId}");
        }

        public static Error ValueIsInvalid(string? name)
        {
            var label = name ?? "Value";
            return new("value.is.invalid", $"{label} is invalid");
        }

        public static Error ValueIsRequried(string? name)
        {
            var label = name ?? "Value";
            return new("value.is.required", $"{label} is required");
        }

        public static Error InvalidLength(string? name = null)
        {
            var label = name == null ? " " : " " + name + " ";
            return new("invalid.string.length", $"invalid{label}length");
        }
    }
}