using PetFamily.Application.Models;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Services;

public interface INotificationService
{
    public Task<Result> Notify(Notification notification, CancellationToken ct);
}