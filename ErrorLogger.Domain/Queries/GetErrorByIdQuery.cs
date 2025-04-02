using ErrorLogger.Domain.Models;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public record GetErrorByIdQuery : IRequest<Error?>
    {
        public Guid Id { get; init; }
    }
}