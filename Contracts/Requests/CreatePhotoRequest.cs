namespace Contracts.Requests;

public record CreatePhotoRequest(
    string Path,
    bool IsMain
    );
