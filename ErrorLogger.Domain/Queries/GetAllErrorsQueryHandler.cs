using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public class GetAllErrorsQueryHandler : IRequestHandler<GetAllErrorsQuery, IEnumerable<Error>>
    {
        private readonly IErrorRepository _errorRepository;

        public GetAllErrorsQueryHandler(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public async Task<IEnumerable<Error>> Handle(GetAllErrorsQuery request, CancellationToken cancellationToken)
        {
            return await _errorRepository.GetAllErrorsAsync(cancellationToken);
        }
    }
}