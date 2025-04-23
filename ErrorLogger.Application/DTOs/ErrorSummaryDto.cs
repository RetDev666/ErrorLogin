namespace ErrorLogger.Application.DTOs;

public class ErrorSummaryDto
{
    public Guid Id { get; set; }
    public string Message { get; set; } 
    public int StatusCode { get; set; }
    public string Status { get; set; } 
    public DateTime Timestamp { get; set; }
}