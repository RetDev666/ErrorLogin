using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Core.Interfaces;
using ErrorLogger.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ErrorLogger.Application.Queries.GetAllErrors
{
    public class GetAllErrorsQueryHandler : IRequestHandler<GetAllErrorsQuery, IEnumerable<Error>>
    {
        private readonly IErrorRepository _errorRepository;
        private readonly ILogger<GetAllErrorsQueryHandler> _logger;

        public GetAllErrorsQueryHandler(
            IErrorRepository errorRepository,
            ILogger<GetAllErrorsQueryHandler> logger)
        {
            _errorRepository = errorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Error>> Handle(GetAllErrorsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Отримання всіх помилок");
            return await _errorRepository.GetAllErrorsAsync(cancellationToken);
        }
    }
}