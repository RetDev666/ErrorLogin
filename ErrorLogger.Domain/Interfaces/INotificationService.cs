using ErrorLogger.Domain.Entities;

namespace ErrorLogger.Domain.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendErrorNotificationAsync(Error error, CancellationToken cancellationToken = default);
    }
}