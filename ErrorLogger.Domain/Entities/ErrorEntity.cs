namespace ErrorLogger.Domain.Entities
{
    public class ErrorEntity
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
        public string Source { get; set; } 
        public int StatusCode { get; set; }
        public int StatusId { get; set; } 
        public DateTime Timestamp { get; set; }
    }
}