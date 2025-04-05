using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using MediatR;

namespace ErrorLogger.Domain.Commands
{
    public class LogErrorCommandHandler : IRequestHandler<LogErrorCommand, Guid>
    {
        private readonly IErrorRepository errorRepository;
        private readonly INotificationService notificationService;

        public LogErrorCommandHandler(
            IErrorRepository errorRepository, 
            INotificationService notificationService)
        {
            this.errorRepository = errorRepository;
            this.notificationService = notificationService;
        }

        public async Task<Guid> Handle(LogErrorCommand request, CancellationToken cancellationToken)
        {
            var error = new Error
            {
                Message = request.Message,
                StackTrace = request.StackTrace,
                Source = request.Source,
                StatusCode = request.StatusCode
            };

            var errorId = await errorRepository.SaveErrorAsync(error, cancellationToken);
            await notificationService.SendErrorNotificationAsync(error, cancellationToken);

            return errorId;
        }
    }
}