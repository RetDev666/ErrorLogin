using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Core.Models;

namespace ErrorLogger.Core.Interfaces
{
    public interface IErrorRepository
    {
        Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default);
        Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default);
        Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default);
    }
}