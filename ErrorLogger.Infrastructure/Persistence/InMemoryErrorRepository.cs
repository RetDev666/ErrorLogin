using System.Collections.Concurrent;
using ErrorLogger.Domain.Entities;
using ErrorLogger.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ErrorLogger.Infrastructure.Persistence
{
   public class InMemoryErrorRepository : IErrorRepository
   {
       private readonly ILogger<InMemoryErrorRepository> logger;
       private readonly ConcurrentDictionary<Guid, Error> errors = new();

       public InMemoryErrorRepository(ILogger<InMemoryErrorRepository> logger)
       {
           this.logger = logger;
       }

       public Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default)
       {
           if (errors.TryAdd(error.Id, error))
           {
              logger.LogInformation("Помилка збережена з ID: {ErrorId}", error.Id);
              return Task.FromResult(error.Id);
           }

           logger.LogWarning("Не вдалося зберегти помилку з ID: {ErrorId}", error.Id);
           throw new InvalidOperationException($"Помилка з ID {error.Id} вже існує");
       }

       public Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default)
       {
           errors.TryGetValue(id, out var error);
           return Task.FromResult(error);
       }

       public Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default)
       {
           return Task.FromResult(errors.Values.AsEnumerable());
       }

       public Task<IEnumerable<Error>> GetErrorsByStatusAsync(ErrorStatus status, CancellationToken cancellationToken = default)
       {
           var filteredErrors = errors.Values.Where(e => e.Status == status);
           return Task.FromResult(filteredErrors);
       }

       public Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default)
       {
           if (errors.TryGetValue(id, out var existingError))
           {
               existingError.Status = status;
               logger.LogInformation("Оновлено статус помилки ID {ErrorId} на {Status}", id, status);
               return Task.FromResult(true);
           }

           logger.LogWarning("Не вдалося оновити статус помилки ID {ErrorId}, помилка не знайдена", id);
           return Task.FromResult(false);
       }

       public Task<bool> DeleteErrorAsync(Guid id, CancellationToken cancellationToken = default)
       {
           if (errors.TryRemove(id, out _))
           {
               logger.LogInformation("Видалено помилку з ID: {ErrorId}", id);
               return Task.FromResult(true);
           }

           logger.LogWarning("Не вдалося видалити помилку ID {ErrorId}, помилка не знайдена", id);
           return Task.FromResult(false);
       }
   }
}