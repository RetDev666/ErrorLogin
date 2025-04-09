using AutoMapper;
using ErrorLogger.Application.DTOs;
using ErrorLogger.Domain.Interfaces;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public class GetErrorByIdQueryHandler : IRequestHandler<GetErrorByIdQuery, ErrorDto?>
    {
        private readonly IErrorRepository errorRepository;
        private readonly IMapper mapper;

        public GetErrorByIdQueryHandler(IErrorRepository errorRepository, IMapper mapper)
        {
            this.errorRepository = errorRepository;
            this.mapper = mapper;
        }

        public async Task<ErrorDto?> Handle(GetErrorByIdQuery request, CancellationToken cancellationToken)
        {
            var error = await errorRepository.GetErrorByIdAsync(request.Id, cancellationToken);
            return error != null ? mapper.Map<ErrorDto>(error) : null;
        }
    }
}