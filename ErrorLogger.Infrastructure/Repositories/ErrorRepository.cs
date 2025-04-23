using ErrorLogger.Domain.Entities;
using ErrorLogger.Domain.Interfaces;
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
        
        public Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default)
        {
            var id = Guid.NewGuid();
            logger.LogInformation("Помилка збережена з ідентифікатором: {ErrorId}", id);
            return Task.FromResult(id);
        }
        
        public Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Запит на помилку з ідентифікатором: {ErrorId}", id);
            return Task.FromResult<Error?>(null);
        }
        
        public Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Запит на всі помилки");
            return Task.FromResult<IEnumerable<Error>>(new List<Error>());
        }
        
        public Task<IEnumerable<Error>> GetErrorsByStatusAsync(ErrorStatus status, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Запит на помилки зі статусом: {Status}", status);
            return Task.FromResult<IEnumerable<Error>>(new List<Error>());
        }
        
        public Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Оновлений статус помилки з ідентифікатором {ErrorId} до {Status}", id, status);
            return Task.FromResult(true);
        }
        
        public Task<bool> DeleteErrorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Видалено помилку з ідентифікатором: {ErrorId}", id);
            return Task.FromResult(true);
        }
    }
}