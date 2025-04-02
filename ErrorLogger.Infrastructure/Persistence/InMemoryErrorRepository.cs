using System;
using System.Collections.Concurrent;
using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ErrorLogger.Infrastructure.Persistence
{
   public class InMemoryErrorRepository : IErrorRepository
   {
       private readonly ILogger<InMemoryErrorRepository> _logger;
       private readonly ConcurrentDictionary<Guid, Error> _errors = new();

       public InMemoryErrorRepository(ILogger<InMemoryErrorRepository> logger)
       {
           _logger = logger;
       }

       public Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default)
       {
           if (_errors.TryAdd(error.Id, error))
           {
               _logger.LogInformation("Помилка збережена з ID: {ErrorId}", error.Id);
               return Task.FromResult(error.Id);
           }

           _logger.LogWarning("Не вдалося зберегти помилку з ID: {ErrorId}", error.Id);
           throw new InvalidOperationException($"Помилка з ID {error.Id} вже існує");
       }

       public Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default)
       {
           _errors.TryGetValue(id, out var error);
           return Task.FromResult(error);
       }

       public Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default)
       {
           return Task.FromResult(_errors.Values.AsEnumerable());
       }

       public Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default)
       {
           if (_errors.TryGetValue(id, out var existingError))
           {
               existingError.Status = status;
               _logger.LogInformation("Оновлено статус помилки ID {ErrorId} на {Status}", id, status);
               return Task.FromResult(true);
           }

           _logger.LogWarning("Не вдалося оновити статус помилки ID {ErrorId}, помилка не знайдена", id);
           return Task.FromResult(false);
       }
   }
}