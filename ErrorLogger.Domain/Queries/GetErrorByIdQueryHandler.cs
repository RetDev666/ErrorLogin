using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public class GetErrorByIdQueryHandler : IRequestHandler<GetErrorByIdQuery, Error?>
    {
        private readonly IErrorRepository errorRepository;

        public GetErrorByIdQueryHandler(IErrorRepository errorRepository)
        {
            this.errorRepository = errorRepository;
        }

        public async Task<Error?> Handle(GetErrorByIdQuery request, CancellationToken cancellationToken)
        {
            return await errorRepository.GetErrorByIdAsync(request.Id, cancellationToken);
        }
    }
}