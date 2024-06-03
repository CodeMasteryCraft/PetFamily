using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Messages;

public record EmailNotification(string Message, Email Email);