using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcMonitorWebApi.Data.Models;
using PcMonitorWebApi.Data;
using PcMonitorWebApi.Servises.Interfaces;

namespace PcMonitorWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputersController : ControllerBase
    {
        private readonly IComputerService _computerService;
        private readonly ILogger<ComputersController> _logger;

        public ComputersController(IComputerService computerService, ILogger<ComputersController> logger)
        {
            _computerService = computerService;
            _logger = logger;
        }

        /// <summary>
        /// Получение компьютера по имени
        /// </summary>
        /// <param name="name">Имя компьютера</param>
        /// <returns>Компьютер или 404 Not Found</returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetComputerByName(string name)
        {
            var computer = await _computerService.GetComputerByNameAsync(name);
            if (computer == null)
            {
                _logger.LogWarning("Компьютер с именем {ComputerName} не найден", name);
                return NotFound();
            }

            return Ok(computer);
        }

        /// <summary>
        /// Получение списка всех компьютеров
        /// </summary>
        /// <returns>Список компьютеров</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllComputers()
        {
            var computers = await _computerService.GetAllComputersAsync();
            if (computers != null)
                return Ok(computers);
            else return NotFound();
        }

        /// <summary>
        /// Добавление или обновление компьютера
        /// </summary>
        /// <param name="computer">Данные компьютера</param>
        /// <returns>Ответ 201 Created или 200 OK</returns>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateComputer([FromBody] Computer computer)
        {
            if (string.IsNullOrWhiteSpace(computer.ComputerName))
            {
                _logger.LogWarning("Попытка добавить компьютер без имени");
                return BadRequest("Имя компьютера не может быть пустым.");
            }

            try
            {
                var existingComputer = await _computerService.GetComputerByNameAsync(computer.ComputerName);

                if (existingComputer != null)
                {
                    var updatedComputer = await _computerService.UpdateComputeAsync(computer);
                    return Ok(updatedComputer);
                }
                else
                {
                    var newComputer = await _computerService.AddComputeAsync(computer);
                    return CreatedAtAction(nameof(GetComputerByName), new { name = newComputer.ComputerName }, newComputer);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении данных компьютера");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
