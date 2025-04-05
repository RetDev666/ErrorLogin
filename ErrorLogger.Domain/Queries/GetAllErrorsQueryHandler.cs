using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public class GetAllErrorsQueryHandler : IRequestHandler<GetAllErrorsQuery, IEnumerable<Error>>
    {
        private readonly IErrorRepository errorRepository;

        public GetAllErrorsQueryHandler(IErrorRepository errorRepository)
        {
            this.errorRepository = errorRepository;
        }

        public async Task<IEnumerable<Error>> Handle(GetAllErrorsQuery request, CancellationToken cancellationToken)
        {
            return await errorRepository.GetAllErrorsAsync(cancellationToken);
        }
    }
}