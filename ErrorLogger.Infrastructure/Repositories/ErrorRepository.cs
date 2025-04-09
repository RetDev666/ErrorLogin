using AutoMapper;
using ErrorLogger.Domain.Entities;
using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using ErrorLogger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErrorLogger.Infrastructure.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly ErrorDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ErrorRepository> logger;
        
        public ErrorRepository(ErrorDbContext dbContext, IMapper mapper, ILogger<ErrorRepository> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        
        public async Task<Guid> SaveErrorAsync(Error error, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = mapper.Map<ErrorEntity>(error);
                await dbContext.Errors.AddAsync(entity, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                logger.LogInformation("Помилку збережено з ID: {ErrorId}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при збереженні помилки");
                throw;
            }
        }
        
        public async Task<Error?> GetErrorByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await dbContext.Errors.FindAsync(new object[] { id }, cancellationToken);
                if (entity == null)
                {
                    logger.LogWarning("Помилку з ID {ErrorId} не знайдено", id);
                    return null;
                }
                
                return mapper.Map<Error>(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при отриманні помилки за ID: {ErrorId}", id);
                throw;
            }
        }
        
        public async Task<IEnumerable<Error>> GetAllErrorsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var entities = await dbContext.Errors.ToListAsync(cancellationToken);
                return mapper.Map<IEnumerable<Error>>(entities);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при отриманні всіх помилок");
                throw;
            }
        }
        
        public async Task<IEnumerable<Error>> GetErrorsByStatusAsync(ErrorStatus status, CancellationToken cancellationToken = default)
        {
            try
            {
                var statusId = (int)status;
                var entities = await dbContext.Errors
                    .Where(e => e.StatusId == statusId)
                    .ToListAsync(cancellationToken);
                
                return mapper.Map<IEnumerable<Error>>(entities);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при отриманні помилок за статусом: {Status}", status);
                throw;
            }
        }
        
        public async Task<bool> UpdateErrorStatusAsync(Guid id, ErrorStatus status, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await dbContext.Errors.FindAsync(new object[] { id }, cancellationToken);
                
                if (entity == null)
                {
                    logger.LogWarning("Помилку з ID {ErrorId} не знайдено для оновлення статусу", id);
                    return false;
                }
                
                entity.StatusId = (int)status;
                dbContext.Errors.Update(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                
                logger.LogInformation("Оновлено статус помилки ID {ErrorId} на {Status}", id, status);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при оновленні статусу помилки ID: {ErrorId}", id);
                throw;
            }
        }
        
        public async Task<bool> DeleteErrorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await dbContext.Errors.FindAsync(new object[] { id }, cancellationToken);
                
                if (entity == null)
                {
                    logger.LogWarning("Помилку з ID {ErrorId} не знайдено для видалення", id);
                    return false;
                }
                
                dbContext.Errors.Remove(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                
                logger.LogInformation("Видалено помилку з ID: {ErrorId}", id);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при видаленні помилки ID: {ErrorId}", id);
                throw;
            }
        }
    }
}