namespace PetFamily.Domain.Common;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty);

    private const string Separator = "||";

    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
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

        public static Error ValueIsRequired(string? name = null)
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
    }

    public static class Volunteers
    {
        public static Error PhotoCountLimit()
        {
            return new("volunteers.photo.limit", "Max photo count limit is 5");
        }

        public static Error FileTypeInvalid(string? fileType)
        {
            return new("invalid.file.type", $"This {fileType}: file type is invalid");
        }public static Error FileLengthInvalid(long? length)
        {
            return new("invalid.file.length", $"This {length}: file length is invalid");
        }
    }

    public static class VolunteersApplications
    {
        public static Error AlreadyApproved() =>
            new("volunteers.applications.already.approved", "Volunteer application has already been approved");
    }

    public static class Users
    {
        public static Error InvalidCredentials()
        {
            return new("users.invalid.credentials", "User's credentials is invalid");
        }
    }
}