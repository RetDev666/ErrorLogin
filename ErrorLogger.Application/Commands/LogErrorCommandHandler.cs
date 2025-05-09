using AutoMapper;
using ErrorLogger.Domain.Entities;
using ErrorLogger.Domain.Interfaces;
using MediatR;

namespace ErrorLogger.Application.Commands
{
    public class LogErrorCommandHandler : IRequestHandler<LogErrorCommand, Guid>
    {
        private readonly IErrorRepository errorRepository;
        private readonly INotificationService notificationService;
        private readonly IMapper mapper;

        public LogErrorCommandHandler(
            IErrorRepository errorRepository, 
            INotificationService notificationService,
            IMapper mapper)
        {
            this.errorRepository = errorRepository;
            this.notificationService = notificationService;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(LogErrorCommand request, CancellationToken cancellationToken)
        {
            var error = mapper.Map<Error>(request);

            var errorId = await errorRepository.SaveErrorAsync(error, cancellationToken);
            await notificationService.SendErrorNotificationAsync(error, cancellationToken);

            return errorId;
        }
    }
}