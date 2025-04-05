using ErrorLogger.Domain.Commands;
using ErrorLogger.Domain.Models;
using ErrorLogger.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLogger.WebApi.Controllers
{
    /// <summary>
    /// Контролер для управління системними помилками
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ErrorsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ErrorsController> logger;

        /// <summary>
        /// Конструктор контролера помилок
        /// </summary>
        /// <param name="mediator">Медіатор для обробки команд</param>
        /// <param name="logger">Логер для реєстрації подій</param>
        public ErrorsController(IMediator mediator, ILogger<ErrorsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Реєстрація нової помилки в системі
        /// </summary>
        /// <param name="command">Дані про помилку</param>
        /// <returns>Результат реєстрації помилки</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LogError([FromBody] LogErrorCommand command)
        {
            try
            {
                var result = await mediator.Send(command);
                return Ok(new { 
                    Id = result, 
                    Message = "Помилку успішно зареєстровано та відправлено в Telegram" 
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при обробці запиту на реєстрацію помилки");
                return StatusCode(500, new { 
                    Message = "Внутрішня помилка сервера", 
                    Error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Отримання деталей конкретної помилки за її ідентифікатором
        /// </summary>
        /// <param name="id">Унікальний ідентифікатор помилки</param>
        /// <returns>Деталі помилки</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Error), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetError(Guid id)
        {
            try
            {
                var error = await mediator.Send(new GetErrorByIdQuery { Id = id });
                if (error == null)
                {
                    return NotFound(new { 
                        Message = $"Помилку з ID {id} не знайдено" 
                    });
                }
                return Ok(error);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при отриманні помилки з ID {ErrorId}", id);
                return StatusCode(500, new { 
                    Message = "Внутрішня помилка сервера", 
                    Error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Отримання списку всіх зареєстрованих помилок
        /// </summary>
        /// <returns>Колекція помилок</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllErrors()
        {
            try
            {
                var errors = await mediator.Send(new GetAllErrorsQuery());
                return Ok(errors);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при отриманні списку помилок");
                return StatusCode(500, new { 
                    Message = "Внутрішня помилка сервера", 
                    Error = ex.Message 
                });
            }
        }
    }
}