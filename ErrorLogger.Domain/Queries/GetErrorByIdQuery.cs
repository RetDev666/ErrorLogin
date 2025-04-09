using ErrorLogger.Domain.DTOs;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public record GetErrorByIdQuery : IRequest<ErrorDto?>
    {
        public Guid Id { get; init; }
    }
}