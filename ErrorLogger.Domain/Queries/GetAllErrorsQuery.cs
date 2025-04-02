using ErrorLogger.Domain.Models;
using MediatR;

namespace ErrorLogger.Domain.Queries
{
    public record GetAllErrorsQuery : IRequest<IEnumerable<Error>>;
}