using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Core.Interfaces;
using ErrorLogger.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ErrorLogger.Application.Queries.GetErrorById
{
    public class GetErrorByIdQueryHandler : IRequestHandler<GetErrorByIdQuery, Error?>
    {
        private readonly IErrorRepository _errorRepository;
        private readonly ILogger<GetErrorByIdQueryHandler> _logger;

        public GetErrorByIdQueryHandler(
            IErrorRepository errorRepository,
            ILogger<GetErrorByIdQueryHandler> logger)
        {
            _errorRepository = errorRepository;
            _logger = logger;
        }

        public async Task<Error?> Handle(GetErrorByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Отримання помилки за ID: {ErrorId}", request.Id);
            return await _errorRepository.GetErrorByIdAsync(request.Id, cancellationToken);
        }
    }
}