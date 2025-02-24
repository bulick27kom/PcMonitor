using Microsoft.EntityFrameworkCore;
using PcMonitorWebApi.Data;
using PcMonitorWebApi.Data.Models;
using PcMonitorWebApi.Servises.Interfaces;

namespace PcMonitorWebApi.Servises
{
    public class ComputerService : IComputerService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ComputerService> _logger;

        public ComputerService(AppDbContext context, ILogger<ComputerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Получить компьютер по имени
        /// </summary>
        public async Task<Computer?> GetComputerByNameAsync(string name)
        {
            return await _context.Computers
                .Include(c => c.Processor)
                .Include(c => c.MemoryModules)
                .Include(c => c.DiskDrives)
                .Include(c => c.GraphicsCards)
                .Include(c => c.NetworkInterfaces)
                .Include(c => c.Group)
                .FirstOrDefaultAsync(c => c.ComputerName == name);
        }

        /// <summary>
        /// Получить все компьютеры
        /// </summary>
        public async Task<IEnumerable<Computer>> GetAllComputersAsync()
        {
            return await _context.Computers
                .Include(c => c.Processor)
                .Include(c => c.MemoryModules)
                .Include(c => c.DiskDrives)
                .Include(c => c.GraphicsCards)
                .Include(c => c.NetworkInterfaces)
                .Include(c => c.Group)
                .ToListAsync();
        }

        /// <summary>
        /// Добавить новый компьютер или обновить существующий
        /// </summary>
        public async Task<Computer> AddComputeAsync(Computer computer)
        {
            var existingComputer = await GetComputerByNameAsync(computer.ComputerName);
            if (existingComputer != null)
            {
                _logger.LogInformation("Компьютер {ComputerName} уже существует. Обновляем данные...", computer.ComputerName);
                return await UpdateComputeAsync(computer);
            }

            _context.Computers.Add(computer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Добавлен новый компьютер {ComputerName}.", computer.ComputerName);

            return computer;
        }

        /// <summary>
        /// Обновить существующий компьютер
        /// </summary>
        public async Task<Computer> UpdateComputeAsync(Computer computer)
        {
            var existingComputer = await GetComputerByNameAsync(computer.ComputerName);
            if (existingComputer == null)
            {
                _logger.LogWarning("Компьютер {ComputerName} не найден. Добавляем как новый...", computer.ComputerName);
                return await AddComputeAsync(computer);
            }

            // Обновляем основные поля
            existingComputer.OS = computer.OS;
            existingComputer.SystemArchitecture = computer.SystemArchitecture;
            existingComputer.WorkgroupOrDomain = computer.WorkgroupOrDomain;
            existingComputer.LastBootTime = computer.LastBootTime;
            existingComputer.GroupId = computer.GroupId;

            // Обновляем связанные сущности
            existingComputer.Processor = computer.Processor;
            existingComputer.MemoryModules = computer.MemoryModules;
            existingComputer.DiskDrives = computer.DiskDrives;
            existingComputer.GraphicsCards = computer.GraphicsCards;
            existingComputer.NetworkInterfaces = computer.NetworkInterfaces;

            _context.Computers.Update(existingComputer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Обновлен компьютер {ComputerName}.", computer.ComputerName);

            return existingComputer;
        }
    }
}
