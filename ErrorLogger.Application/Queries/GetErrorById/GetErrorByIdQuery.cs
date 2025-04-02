using System;
using ErrorLogger.Core.Models;
using MediatR;

namespace ErrorLogger.Application.Queries.GetErrorById
{
    public record GetErrorByIdQuery : IRequest<Error?>
    {
        public Guid Id { get; init; }
    }
}