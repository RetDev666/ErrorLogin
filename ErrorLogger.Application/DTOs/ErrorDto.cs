using ErrorLogger.Domain.Models;

namespace ErrorLogger.Application.DTOs
{
    public class ErrorDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } 
        public string? StackTrace { get; set; }
        public string Source { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; set; } 
        public DateTime Timestamp { get; set; }
    }
    
    public class ErrorSummaryDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } 
        public int StatusCode { get; set; }
        public string Status { get; set; } 
        public DateTime Timestamp { get; set; }
    }
    
    public class CreateErrorDto
    {
        public string Message { get; set; } 
        public string? StackTrace { get; set; }
        public string Source { get; set; } 
        public int StatusCode { get; set; }
    }
}