using ErrorLogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorLogger.Infrastructure.Data
{
    public class ErrorDbContext : DbContext
    {
        public ErrorDbContext(DbContextOptions<ErrorDbContext> options) : base(options)
        {
        }
        
        public DbSet<ErrorEntity> Errors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Конфігурація для ErrorEntity
            modelBuilder.Entity<ErrorEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.Source).IsRequired();
                // Додаткові налаштування
            });
        }
    }
}