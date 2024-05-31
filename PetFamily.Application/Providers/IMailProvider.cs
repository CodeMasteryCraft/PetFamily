namespace PetFamily.Application.Providers;

public interface IMailProvider
{
    Task SendMessage(string message, Guid userId);
}