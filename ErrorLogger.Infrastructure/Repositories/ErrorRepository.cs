using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ErrorLogger.Infrastructure.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly ILogger<ErrorRepository> logger;
        
        public ErrorRepository(ILogger<ErrorRepository> logger)
        {
            this.logger = logger;
        }
        
        public async Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default)
        {
            var id = Guid.NewGuid();
            logger.LogInformation("Помилку збережено з ID: {ErrorId}", id);
            return id;
        }
        
        public async Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Запит помилки за ID: {ErrorId}", id);
            return null;
        }
        
        public async Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Запит на отримання всіх помилок");
            return new List<Error>();
        }
        
        public async Task<IEnumerable<Error>> GetErrorsByStatusAsync(ErrorStatus status, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Запит помилок за статусом: {Status}", status);
            return new List<Error>();
        }
        
        public async Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Оновлено статус помилки ID {ErrorId} на {Status}", id, status);
            return true;
        }
        
        public async Task<bool> DeleteErrorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Видалено помилку з ID: {ErrorId}", id);
            return true;
        }
    }
}