using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Core.Interfaces;
using ErrorLogger.Core.Models;

namespace ErrorLogger.Infrastructure.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private static readonly List<Error> _errors = new();

        public async Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default)
        {
            _errors.Add(error);
            return error.Id;
        }

        public async Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _errors.Find(e => e.Id == id);
        }

        public async Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default)
        {
            return _errors;
        }

        public async Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default)
        {
            var error = _errors.Find(e => e.Id == id);
            if (error == null) return false;
            
            error.Status = status;
            return true;
        }
    }
}