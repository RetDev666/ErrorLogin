using ErrorLogger.Domain.Entities;

namespace ErrorLogger.Domain.Interfaces
{
    public interface IErrorRepository
    {
        Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default);
        
        Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default);
        
        Task<IEnumerable<Error>> GetErrorsByStatusAsync(ErrorStatus status, CancellationToken cancellationToken = default);
        
        Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default);
        
        Task<bool> DeleteErrorAsync(Guid id, CancellationToken cancellationToken = default);
    }
}