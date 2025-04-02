using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Core.Models;

namespace ErrorLogger.Core.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendErrorNotificationAsync(Error error, CancellationToken cancellationToken = default);
    }
}