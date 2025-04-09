using ErrorLogger.Application.DTOs;
using MediatR;

namespace ErrorLogger.Application.Queries
{
    public record GetAllErrorsQuery : IRequest<IEnumerable<ErrorDto>>;
}