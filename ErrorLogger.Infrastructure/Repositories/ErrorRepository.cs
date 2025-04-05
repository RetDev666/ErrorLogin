using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;

namespace ErrorLogger.Infrastructure.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly List<Error> errors = new();

        public async Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default)
        {
            errors.Add(error);
            return error.Id;
        }

        public async Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return errors.Find(e => e.Id == id);
        }

        public async Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default)
        {
            return errors;
        }

        public async Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default)
        {
            var error = errors.Find(e => e.Id == id);
            if (error == null) return false;
            
            error.Status = status;
            return true;
        }
    }
}