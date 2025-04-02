using System.Collections.Generic;
using ErrorLogger.Core.Models;
using MediatR;

namespace ErrorLogger.Application.Queries.GetAllErrors
{
    public record GetAllErrorsQuery : IRequest<IEnumerable<Error>>;
}