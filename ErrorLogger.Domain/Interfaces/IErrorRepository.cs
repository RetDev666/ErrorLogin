using ErrorLogger.Domain.Models;

namespace ErrorLogger.Domain.Interfaces
{
    public interface IErrorRepository
    {
        Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default);
        Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default);
        Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default);
    }
}