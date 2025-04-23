namespace ErrorLogger.Application.DTOs;

public class CreateErrorDto
{
    public string Message { get; set; } 
    public string? StackTrace { get; set; }
    public string Source { get; set; } 
    public int StatusCode { get; set; }
}