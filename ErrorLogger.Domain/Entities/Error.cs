namespace ErrorLogger.Domain.Entities
{
    public enum ErrorStatus
    {
        New,
        Processed,
        Closed
    }

    public class Error
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public ErrorStatus Status { get; set; } = ErrorStatus.New;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}