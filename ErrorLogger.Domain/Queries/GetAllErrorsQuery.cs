using ErrorLogger.Domain.DTOs;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public record GetAllErrorsQuery : IRequest<IEnumerable<ErrorDto>>;
}