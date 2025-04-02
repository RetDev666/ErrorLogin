using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public class GetErrorByIdQueryHandler : IRequestHandler<GetErrorByIdQuery, Error?>
    {
        private readonly IErrorRepository _errorRepository;

        public GetErrorByIdQueryHandler(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public async Task<Error?> Handle(GetErrorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _errorRepository.GetErrorByIdAsync(request.Id, cancellationToken);
        }
    }
}