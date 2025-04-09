using AutoMapper;
using ErrorLogger.Domain.DTOs;
using ErrorLogger.Domain.Interfaces;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public class GetAllErrorsQueryHandler : IRequestHandler<GetAllErrorsQuery, IEnumerable<ErrorDto>>
    {
        private readonly IErrorRepository errorRepository;
        private readonly IMapper mapper;

        public GetAllErrorsQueryHandler(IErrorRepository errorRepository, IMapper mapper)
        {
            this.errorRepository = errorRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ErrorDto>> Handle(GetAllErrorsQuery request, CancellationToken cancellationToken)
        {
            var errors = await errorRepository.GetAllErrorsAsync(cancellationToken);
            return mapper.Map<IEnumerable<ErrorDto>>(errors);
        }
    }
}