using System.Net;
using System.Text.Json;
using ErrorLogger.Domain.Commands;
using MediatR;

namespace ErrorLogger.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IMediator _mediator;

        public ErrorHandlingMiddleware(
            RequestDelegate next, 
            ILogger<ErrorHandlingMiddleware> logger,
            IMediator mediator)
        {
            _next = next;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Необроблена помилка: {ErrorMessage}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Встановлюємо код статусу і тип вмісту для відповіді
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Логуємо помилку через MediatR/CQRS для відправки в Telegram
            try
            {
                await _mediator.Send(new LogErrorCommand
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    Source = exception.Source ?? context.Request.Path,
                    StatusCode = context.Response.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при спробі зареєструвати помилку через MediatR");
            }

            // Формуємо відповідь
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Виникла помилка при обробці запиту.",
                Error = exception.Message,
                Path = context.Request.Path
            };

            // Серіалізуємо відповідь у JSON
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}