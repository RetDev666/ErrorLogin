using MediatR;

namespace ErrorLogger.Domain.Commands
{
    public record LogErrorCommand : IRequest<Guid>
    {
        public string Message { get; init; } = string.Empty;
        public string? StackTrace { get; init; }
        public string Source { get; init; } = string.Empty;
        public int StatusCode { get; init; }
    }
}