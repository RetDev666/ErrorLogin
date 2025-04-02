using System;
using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Core.Interfaces;
using ErrorLogger.Core.Models;
using MediatR;

namespace ErrorLogger.Application.Commands.LogError
{
    public class LogErrorCommandHandler : IRequestHandler<LogErrorCommand, Guid>
    {
        private readonly IErrorRepository _errorRepository;
        private readonly INotificationService _notificationService;

        public LogErrorCommandHandler(
            IErrorRepository errorRepository, 
            INotificationService notificationService)
        {
            _errorRepository = errorRepository;
            _notificationService = notificationService;
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

            var errorId = await _errorRepository.SaveErrorAsync(error, cancellationToken);
            await _notificationService.SendErrorNotificationAsync(error, cancellationToken);

            return errorId;
        }
    }
}