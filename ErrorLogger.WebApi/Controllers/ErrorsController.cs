using System;
using System.Threading.Tasks;
using ErrorLogger.Application.Commands.LogError;
using ErrorLogger.Application.Queries.GetAllErrors;
using ErrorLogger.Application.Queries.GetErrorById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErrorLogger.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(IMediator mediator, ILogger<ErrorsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> LogError([FromBody] LogErrorCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { Id = result, Message = "Помилку успішно зареєстровано та відправлено в Telegram" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при обробці запиту на реєстрацію помилки");
                return StatusCode(500, new { Message = "Внутрішня помилка сервера", Error = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetError(Guid id)
        {
            try
            {
                var error = await _mediator.Send(new GetErrorByIdQuery { Id = id });
                if (error == null)
                {
                    return NotFound(new { Message = $"Помилку з ID {id} не знайдено" });
                }
                return Ok(error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при отриманні помилки з ID {ErrorId}", id);
                return StatusCode(500, new { Message = "Внутрішня помилка сервера", Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllErrors()
        {
            try
            {
                var errors = await _mediator.Send(new GetAllErrorsQuery());
                return Ok(errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при отриманні списку помилок");
                return StatusCode(500, new { Message = "Внутрішня помилка сервера", Error = ex.Message });
            }
        }
    }
}