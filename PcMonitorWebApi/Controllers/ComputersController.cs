using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcMonitorWebApi.Data.Models;
using PcMonitorWebApi.Data;

namespace PcMonitorWebApi.Controllers
{
    [ApiController] // Атрибут, указывающий, что данный класс является API-контроллером
    [Route("api/[controller]")] // Определяет маршрут: api/computers
    public class ComputersController : ControllerBase
    {
        private readonly AppDbContext _context; // Контекст базы данных
        private readonly ILogger<ComputersController> _logger; // Логгер для записи информации и ошибок

        // Конструктор контроллера, принимающий зависимости через внедрение зависимостей (Dependency Injection)
        public ComputersController(AppDbContext context, ILogger<ComputersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Добавление нового компьютера в базу данных
        /// </summary>
        /// <param name="computer">Объект компьютера, переданный в теле запроса</param>
        /// <returns>Ответ 201 Created с добавленным объектом или 500 Internal Server Error в случае ошибки</returns>
        [HttpPost]
        public async Task<IActionResult> PostComputer([FromBody] Computer computer)
        {
            try
            {
                _context.Computers.Add(computer); // Добавляем объект в контекст
                await _context.SaveChangesAsync(); // Асинхронно сохраняем изменения в базе данных

                // Возвращаем статус 201 Created с ссылкой на созданный объект
                return CreatedAtAction(nameof(GetComputer), new { id = computer.Id }, computer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving computer data."); // Логируем ошибку
                return StatusCode(500, "Internal server error"); // Возвращаем ошибку сервера
            }
        }

        /// <summary>
        /// Получение информации о конкретном компьютере по ID
        /// </summary>
        /// <param name="id">Идентификатор компьютера</param>
        /// <returns>Объект компьютера или 404 Not Found, если не найден</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComputer(int id)
        {
            var computer = await _context.Computers
                .Include(c => c.Processor)         // Загружаем процессор
                .Include(c => c.MemoryModules)     // Загружаем модули памяти
                .Include(c => c.DiskDrives)        // Загружаем диски
                .Include(c => c.GraphicsCards)     // Загружаем видеокарты
                .Include(c => c.NetworkInterfaces) // Загружаем сетевые интерфейсы
                .Include(c => c.Group)             // Загружаем группу
                .FirstOrDefaultAsync(c => c.Id == id);

            if (computer == null)
            {
                return NotFound();
            }

            return Ok(computer);
        }

        /// <summary>
        /// Получение списка всех компьютеров в базе данных
        /// </summary>
        /// <returns>Список всех компьютеров</returns>
        [HttpGet]
        public async Task<IActionResult> GetComputers()
        {
            var computers = await _context.Computers
                .Include(c => c.Processor)         // Загружаем процессор
                .Include(c => c.MemoryModules)     // Загружаем модули памяти
                .Include(c => c.DiskDrives)        // Загружаем диски
                .Include(c => c.GraphicsCards)     // Загружаем видеокарты
                .Include(c => c.NetworkInterfaces) // Загружаем сетевые интерфейсы
                .Include(c => c.Group)             // Загружаем группу
                .ToListAsync(); // Получаем список компьютеров из базы данных

            return Ok(computers);
        }

    }
}
